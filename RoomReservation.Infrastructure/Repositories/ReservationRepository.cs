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
            await _dbSet.AddAsync(reservation);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to create reservation");

                return Result<Reservation>.Failure("Failed to create reservation", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            return Result<Reservation>.Success(reservation);
        }

        public async Task<Result<bool>> DeleteAsync(int reservationId, int userId)
        {

            var reservation = await _dbSet.FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
            {
                _logger.LogWarning($"Reservation not found ${reservationId}");

                return Result<bool>.Failure($"Reservation not found ${reservationId}", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            if (!CheckUserAuthorization(reservation, userId))
            {
                _logger.LogError($"User {userId} is not authorized to update reservation {reservation.Id}");

                return Result<bool>.Failure($"User {userId} is not authorized to update reservation {reservation.Id}", System.Net.HttpStatusCode.Unauthorized);
            }

            _dbSet.Remove(reservation);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError($"Failed to delete reservation ${reservationId}");

                return Result<bool>.Failure($"Failed to delete reservation ${reservationId}", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            return Result<bool>.Success();
        }

        public async Task<Result<List<Reservation>>> GetListAsync()
        {
            var reservations = await _dbSet
                .Include(x => x.Room)
                .ToListAsync();

            if (reservations == null || !reservations.Any())
            {
                _logger.LogWarning("No reservations found");

                return Result<List<Reservation>>.Failure("No reservations found", System.Net.HttpStatusCode.NotFound);
            }

            return Result<List<Reservation>>.Success(reservations);

        }

        public async Task<Result<Reservation>> UpdateAsync(UpdateReservationRequest reservation, int userId)
        {
            var reservationEntity = await _dbSet.FirstOrDefaultAsync(r => r.Id == reservation.Id);

            if (reservationEntity == null)
            {
                _logger.LogWarning($"Reservation not found ${reservation.Id}");
                return Result<Reservation>.Failure($"Reservation not found ${reservation.Id}", System.Net.HttpStatusCode.NotFound);
            }

            if (!CheckUserAuthorization(reservationEntity, userId))
            {
                _logger.LogError($"User {userId} is not authorized to update reservation {reservationEntity.Id}");
                return Result<Reservation>.Failure($"User {userId} is not authorized to update reservation {reservationEntity.Id}", System.Net.HttpStatusCode.Unauthorized);
            }

            UpdateReservationProperties(reservation, reservationEntity);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError($"Failed to update reservation ${reservation.Id}");

                return Result<Reservation>.Failure($"Failed to update reservation ${reservation.Id}", System.Net.HttpStatusCode.UnprocessableEntity);
            }

            return Result<Reservation>.Success(reservationEntity);
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

        public async Task<bool> UserHasReservation(int userId, DateTime startTime, DateTime endTime)
        {
            return await _dbSet
                  .AnyAsync(r =>
                      r.UserId == userId
                      && r.StartDate < endTime
                      && r.EndDate > startTime);

        }
    }
}
