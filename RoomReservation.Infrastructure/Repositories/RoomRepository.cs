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

        public async Task<Result<Room>> CreateAsync(Room room)
        {
            await _dbSet.AddAsync(room);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to create room");

                return Result<Room>.Failure("Failed to create room", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            return Result<Room>.Success(room);
        }

        public async Task<Result> DeleteAsync(int roomId)
        {
            var room = await _dbSet.FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                _logger.LogError($"Room not found ${roomId}");


                return Result.Failure($"Room not found ${roomId}", System.Net.HttpStatusCode.NotFound);
            }

            _dbSet.Remove(room);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError($"Failed to delete room ${roomId}");

                return Result.Failure($"Failed to delete room ${roomId}", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            return Result.Success();
        }

        public async Task<Result<Room>> GetByNameAsync(string name)
        {
            var room = await _dbSet.Where(x => x.Name.ToLower() == name.ToLower())
                 .Include(ri => ri.RoomsEquipments)
                     .ThenInclude(i => i.Equipment)
                 .Include(x => x.RoomReservationLimit)
             .FirstOrDefaultAsync();

            if (room == null)
            {
                _logger.LogError($"Room not found {name}");
                return Result<Room>.Failure($"Room not found {name}", System.Net.HttpStatusCode.NotFound);
            }

            return Result<Room>.Success(room);
        }

        public async Task<Result<List<Room>>> GetListAsync(RoomFilter roomFilter)
        {
            var roomsQuery = _dbSet
                .Include(ri => ri.RoomsEquipments)
                    .ThenInclude(i => i.Equipment)
                .Include(x => x.RoomReservationLimit).AsQueryable();

            ApplyFilters(roomFilter, ref roomsQuery);

            var rooms = await roomsQuery.ToListAsync();

            if (rooms == null)
            {
                _logger.LogError("Failed to get rooms");
                return Result<List<Room>>.Failure("Failed to get rooms", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            if (rooms.Count == 0)
            {
                _logger.LogError("No rooms found");
                return Result<List<Room>>.Failure("No rooms found", System.Net.HttpStatusCode.NotFound);
            }

            return Result<List<Room>>.Success(rooms);
        }

        public async Task<Result<Room>> UpdateAsync(RoomDto roomDto)
        {
            var roomEntity = await _dbSet
                .Where(r => r.Id == roomDto.Id)
                .Include(x => x.RoomReservationLimit)
                .FirstOrDefaultAsync();

            if (roomEntity == null)
            {
                _logger.LogError($"Room not found ${roomDto.Id}");
                return Result<Room>.Failure($"Room not found ${roomDto.Id}", System.Net.HttpStatusCode.NotFound);
            }

            UpdateRoomProperties(roomDto, roomEntity);

            UpdateRoomReservationLimit(roomDto, roomEntity);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to update room");
                return Result<Room>.Failure("Failed to update room", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            return Result<Room>.Success(roomEntity);
        }

        private static void UpdateRoomProperties(RoomDto room, Room roomEntity)
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

        public async Task<Result<List<Room>>> GetAvalibilityRoomAsync(RoomAvalibilityRequest roomAvalibilityRequest)
        {
            var roomsQuery = _dbSet
                .Where(r =>
                    !r.Reservations.Any(re => re.StartDate < roomAvalibilityRequest.AvailableTo && re.EndDate > roomAvalibilityRequest.AvailableFrom))
                .Include(r => r.RoomsEquipments)
                    .ThenInclude(r => r.Equipment)
                .Include(rrl => rrl.RoomReservationLimit).AsQueryable();

            ApplyFilters(roomAvalibilityRequest, ref roomsQuery);

            var rooms = await roomsQuery.ToListAsync();

            if (rooms == null)
            {
                _logger.LogError("Failed to get rooms");
                return Result<List<Room>>.Failure("Failed to get rooms", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            if (rooms.Count == 0)
            {
                _logger.LogError("No rooms found");
                return Result<List<Room>>.Failure("No rooms found", System.Net.HttpStatusCode.NotFound);
            }

            return Result<List<Room>>.Success(rooms);
        }

        private void ApplyFilters(RoomFilter roomAvalibilityRequest, ref IQueryable<Room> roomsQuery)
        {
            if (roomAvalibilityRequest.Capacity != null)
                roomsQuery = roomsQuery.Where(r => r.Capacity >= roomAvalibilityRequest.Capacity);
            if (roomAvalibilityRequest.TableCount != null)
                roomsQuery = roomsQuery.Where(r => r.TableCount >= roomAvalibilityRequest.TableCount);
            if (roomAvalibilityRequest.RoomLayout != null)
                roomsQuery = roomsQuery.Where(r => r.RoomLayout == roomAvalibilityRequest.RoomLayout);
            if (roomAvalibilityRequest.EquipmentIds != null && roomAvalibilityRequest.EquipmentIds.Length > 0)
                roomsQuery = roomsQuery.Where(r => r.RoomsEquipments.Any(re => roomAvalibilityRequest.EquipmentIds.Contains(re.EquipmentId)));
        }

        public async Task<Result> IsRoomAvailableAsync(int roomId, DateTime startDate, DateTime endDate, int? reservationId = null)
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
                return LogAndReturnFailure($"Room {room.Name}  reserved ", HttpStatusCode.Conflict);

            if (!IsReservationWithinLimits(room.RoomReservationLimit, startDate, endDate))
                return LogAndReturnFailure($"Room {room.Name} reservation is not in limits", HttpStatusCode.BadRequest);

            return Result.Success();
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
            return durationMinutes >= limit.MinTime && durationMinutes <= limit.MaxTime;
        }

        private Result LogAndReturnFailure(string message, HttpStatusCode statusCode)
        {
            _logger.LogWarning(message);
            return Result.Failure(message, statusCode);
        }
    }
}
