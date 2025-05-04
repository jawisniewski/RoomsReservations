using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
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
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Reservation> _dbSet;
        private readonly ILogger<ReservationRepository> _logger;
        public ReservationRepository(AppDbContext context, ILogger<ReservationRepository> logger)
        {
            _context = context;
            _dbSet = context.Set<Reservation>();
            _logger = logger;

        }

        public async Task<Result<Reservation>> CreateAsync(Reservation reservation)
        {
            try
            {
                await _dbSet.AddAsync(reservation);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    _logger.LogError("Failed to create reservation");

                    return Result<Reservation>.Failure("Failed to create reservation", HttpStatusCode.UnprocessableEntity);
                }

                return Result<Reservation>.Success(reservation);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create reservation");
                return Result<Reservation>.Failure("Failed to create reservation", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create reservation error");
                return Result<Reservation>.Failure(ex, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Result> DeleteAsync(int reservationId, int userId)
        {
            try
            {
                var reservation = await _dbSet.FirstOrDefaultAsync(r => r.Id == reservationId);

                if (reservation == null)
                {
                    _logger.LogWarning($"Reservation not found ${reservationId}");

                    return Result.Failure($"Reservation not found ${reservationId}", HttpStatusCode.NotFound);
                }

                if (!CheckUserAuthorization(reservation, userId))
                {
                    _logger.LogError($"User {userId} is not authorized to update reservation {reservation.Id}");

                    return Result.Failure($"User {userId} is not authorized to update reservation {reservation.Id}", HttpStatusCode.Forbidden);
                }

                _dbSet.Remove(reservation);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    _logger.LogError($"Failed to delete reservation ${reservationId}");

                    return Result.Failure($"Failed to delete reservation ${reservationId}", HttpStatusCode.UnprocessableEntity);
                }

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to delete reservation");
                return Result.Failure("Failed to delete reservation", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete reservation error");
                return Result.Failure("Delete reservation error", HttpStatusCode.BadRequest);
            }
        }
        public async Task<Result<List<Reservation>>> GetListAsync()
        {
            try
            {
                var reservations = await _dbSet
                    .Include(x => x.Room)
                        .ThenInclude(r => r.RoomEquipments)
                    .Include(x => x.Room)
                        .ThenInclude(r => r.RoomReservationLimit)
                    .ToListAsync();

                return Result<List<Reservation>>.Success(reservations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get reservations error");
                return Result<List<Reservation>>.Failure(ex, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Result<Reservation>> UpdateAsync(UpdateReservationRequest reservation, int userId)
        {
            try
            {
                var reservationEntity = await _dbSet.FirstOrDefaultAsync(r => r.Id == reservation.Id);

                if (reservationEntity == null)
                {
                    _logger.LogWarning($"Reservation not found ${reservation.Id}");
                    return Result<Reservation>.Failure($"Reservation not found ${reservation.Id}", HttpStatusCode.NotFound);
                }

                if (!CheckUserAuthorization(reservationEntity, userId))
                {
                    _logger.LogError($"User {userId} is not authorized to update reservation {reservationEntity.Id}");
                    return Result<Reservation>.Failure($"User {userId} is not authorized to update reservation {reservationEntity.Id}", HttpStatusCode.Forbidden);
                }

                UpdateReservationProperties(reservation, reservationEntity);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    _logger.LogError($"Failed to update reservation ${reservation.Id}");

                    return Result<Reservation>.Failure($"Failed to update reservation ${reservation.Id}", HttpStatusCode.UnprocessableEntity);
                }

                return Result<Reservation>.Success(reservationEntity);
            }

            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to update reservation");
                return Result<Reservation>.Failure("Failed to update reservation", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update reservation error");
                return Result<Reservation>.Failure(ex, HttpStatusCode.BadRequest);
            }
        }

        private static void UpdateReservationProperties(UpdateReservationRequest reservation, Reservation reservationEntity)
        {
            reservationEntity.StartDate = reservation.StartDate;
            reservationEntity.EndDate = reservation.EndDate;
            reservationEntity.RoomId = reservation.RoomId;
        }

        private bool CheckUserAuthorization(Reservation reservationEntity, int userId)
        {
            if (reservationEntity.UserId != userId)
                return false;

            return true;
        }

        public async Task<bool> UserHasReservationAsync(int userId, DateTime startTime, DateTime endTime, int? reservationId)
        {
            return await _dbSet
                  .AnyAsync(r =>
                      r.UserId == userId
                      && r.StartDate < endTime
                      && r.EndDate > startTime
                      && reservationId != r.Id);

        }
    }
}
