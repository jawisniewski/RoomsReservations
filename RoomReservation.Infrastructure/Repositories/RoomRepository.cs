using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Domain.Entities;
using RoomReservation.Infrastructure.Context;
using RoomReservation.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Room> _dbSet;
        private readonly ILogger<RoomRepository> _logger;
        public RoomRepository(AppDbContext context, ILogger<RoomRepository> logger)
        {
            _context = context;
            _dbSet = context.Set<Room>();
            _logger = logger;

        }
        private bool IsOverlappingReservation(IEnumerable<Reservation> reservations, DateTime startDate, DateTime endDate, int? reservationId)
        {
            return reservations.Any(r => r.StartDate < endDate && r.EndDate > startDate && r.Id != reservationId);
        }

        private bool IsReservationWithinLimits(RoomReservationLimit limit, DateTime startDate, DateTime endDate)
        {
            if (limit == null)
                return true;

            var durationMinutes = (endDate - startDate).TotalMinutes;
            return (limit.MinTime == 0 || durationMinutes >= limit.MinTime) && (limit.MaxTime == 0 || durationMinutes <= limit.MaxTime);
        }

        private Result LogAndReturnFailure(string message, HttpStatusCode statusCode)
        {
            _logger.LogWarning(message);
            return Result.Failure(message, statusCode);
        }

        private void ApplyFilters(RoomFilter roomAvalibilityRequest, ref IQueryable<Room> roomsQuery)
        {
            if (roomAvalibilityRequest.Capacity != null)
                roomsQuery = roomsQuery.Where(r => r.Capacity >= roomAvalibilityRequest.Capacity);
            if (roomAvalibilityRequest.TableCount != null)
                roomsQuery = roomsQuery.Where(r => r.TableCount >= roomAvalibilityRequest.TableCount);
            if (roomAvalibilityRequest.RoomLayout != null)
                roomsQuery = roomsQuery.Where(r => r.RoomLayout == roomAvalibilityRequest.RoomLayout);
            if (roomAvalibilityRequest.EquipmentTypes != null && roomAvalibilityRequest.EquipmentTypes.Length > 0)
                roomsQuery = roomsQuery.Where(r => roomAvalibilityRequest.EquipmentTypes.Select(e => (int)e).ToArray().All(z => r.RoomsEquipments.Where(x => x.Quantity > 0).Select(re => re.EquipmentId).Contains(z)));
        }
        private void UpdateRoomProperties(RoomDto room, Room roomEntity)
        {
            roomEntity.Name = room.Name;
            roomEntity.TableCount = room.TableCount;
            roomEntity.Capacity = room.Capacity;
            roomEntity.RoomLayout = room.RoomLayout;
        }

        private void UpdateRoomReservationLimit(RoomDto room, Room roomEntity)
        {
            if (room.RoomReservationLimit != null && roomEntity.RoomReservationLimit != null)
            {
                roomEntity.RoomReservationLimit.MinTime = room.RoomReservationLimit.MinTime;
                roomEntity.RoomReservationLimit.MaxTime = room.RoomReservationLimit.MaxTime;
            }
            else if (room.RoomReservationLimit != null)
            {
                roomEntity.RoomReservationLimit = new RoomReservationLimit()
                {
                    RoomId = roomEntity.Id,
                    Id = 0,
                    MinTime = room.RoomReservationLimit.MinTime,
                    MaxTime = room.RoomReservationLimit.MaxTime
                };
            }
        }
        private void UpdateRoomEquipments(RoomDto room, Room roomEntity)
        {
            if (roomEntity.RoomsEquipments != null)
            {
                roomEntity.RoomsEquipments.RemoveAll(re => !room.RoomsEquipments.Select(rre => rre.Id).Contains(re.Id));
            }

            if (roomEntity.RoomsEquipments is null)
                roomEntity.RoomsEquipments = [];

            foreach (var roomEquipment in room.RoomsEquipments.DistinctBy(x => x.EquipmentType))
            {

                var existingRoomEquipment = roomEntity.RoomsEquipments.FirstOrDefault(re => re.Id == roomEquipment.Id);
                if (existingRoomEquipment != null)
                {
                    existingRoomEquipment.Quantity = roomEquipment.Quantity;
                    existingRoomEquipment.EquipmentId = (int)roomEquipment.EquipmentType;
                    continue;
                }

                roomEntity.RoomsEquipments.Add(new RoomEquipment()
                {
                    Id = 0,
                    EquipmentId = (int)roomEquipment.EquipmentType,
                    RoomId = roomEntity.Id,
                    Quantity = roomEquipment.Quantity
                });
            }
        }
        public async Task<Result<Room>> CreateAsync(Room room)
        {
            try
            {
                await _dbSet.AddAsync(room);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    _logger.LogError("Failed to create room");

                    return Result<Room>.Failure("Failed to create room", HttpStatusCode.UnprocessableEntity);
                }

                return Result<Room>.Success(room);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create room");
                return Result<Room>.Failure("Failed to create room", HttpStatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create room error");
                return Result<Room>.Failure(ex, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Result> DeleteAsync(int roomId)
        {
            try
            {
                var room = await _dbSet.FirstOrDefaultAsync(r => r.Id == roomId);

                if (room == null)
                {
                    _logger.LogError($"Room not found ${roomId}");


                    return Result.Failure($"Room not found ${roomId}", HttpStatusCode.NotFound);
                }

                _dbSet.Remove(room);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    _logger.LogError($"Failed to delete room ${roomId}");

                    return Result.Failure($"Failed to delete room ${roomId}", HttpStatusCode.UnprocessableEntity);
                }

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to delete room");
                return Result.Failure("Failed to delete room", HttpStatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete room error");
                return Result.Failure(ex, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Result<List<Room>>> GetListAsync(RoomFilter roomFilter)
        {
            try
            {
                var roomsQuery = _dbSet
                    .Include(ri => ri.RoomsEquipments)
                        .ThenInclude(i => i.Equipment)
                    .Include(x => x.RoomReservationLimit).AsQueryable();

                ApplyFilters(roomFilter, ref roomsQuery);

                var rooms = await roomsQuery.ToListAsync();

                return Result<List<Room>>.Success(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get rooms error");
                return Result<List<Room>>.Failure("Get rooms error", HttpStatusCode.BadRequest);
            }
        }

        public async Task<Result<Room>> UpdateAsync(RoomDto roomDto)
        {
            try
            {
                var roomEntity = await _dbSet
                    .Where(r => r.Id == roomDto.Id)
                    .Include(x => x.RoomReservationLimit)
                    .Include(r => r.RoomsEquipments)
                    .FirstOrDefaultAsync();

                if (roomEntity == null)
                {
                    _logger.LogError($"Room not found ${roomDto.Id}");
                    return Result<Room>.Failure($"Room not found ${roomDto.Id}", HttpStatusCode.NotFound);
                }

                UpdateRoomProperties(roomDto, roomEntity);
                UpdateRoomReservationLimit(roomDto, roomEntity);
                UpdateRoomEquipments(roomDto, roomEntity);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    _logger.LogError("Failed to update room");
                    return Result<Room>.Failure("Failed to update room", HttpStatusCode.UnprocessableEntity);
                }

                return Result<Room>.Success(roomEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to update room");
                return Result<Room>.Failure("Failed to update room", HttpStatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update room error");
                return Result<Room>.Failure(ex, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Result<List<Room>>> GetAvalibilityRoomAsync(RoomAvalibilityRequest roomAvalibilityRequest)
        {
            try
            {
                var roomsQuery = _dbSet
                    .Where(r =>
                        !r.Reservations.Any(re => re.StartDate < roomAvalibilityRequest.AvailableTo && re.EndDate > roomAvalibilityRequest.AvailableFrom))
                    .Include(r => r.RoomsEquipments)
                        .ThenInclude(r => r.Equipment)
                    .Include(rrl => rrl.RoomReservationLimit).AsQueryable();

                ApplyFilters(roomAvalibilityRequest, ref roomsQuery);

                var rooms = await roomsQuery.ToListAsync();

                return Result<List<Room>>.Success(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get rooms error");
                return Result<List<Room>>.Failure(ex, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Result> IsRoomAvailableAsync(int roomId, DateTime startDate, DateTime endDate, int? reservationId = null)
        {
            try
            {
                var room = await _dbSet
                    .Include(r => r.Reservations)
                    .Include(r => r.RoomReservationLimit)
                    .FirstOrDefaultAsync(r => r.Id == roomId);

                if (room == null)
                    return LogAndReturnFailure($"Room not found {roomId}", HttpStatusCode.NotFound);

                if (room.Reservations == null || !room.Reservations.Any())
                    return Result.Success();

                if (IsOverlappingReservation(room.Reservations, startDate, endDate, reservationId))
                    return LogAndReturnFailure($"Room {room.Name}  reserved ", HttpStatusCode.BadRequest);

                if (!IsReservationWithinLimits(room.RoomReservationLimit, startDate, endDate))
                    return LogAndReturnFailure($"Room {room.Name} reservation is not in limits", HttpStatusCode.BadRequest);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Check room availability error");
                return Result.Failure(ex, HttpStatusCode.BadRequest);
            }
        }
    }
}
