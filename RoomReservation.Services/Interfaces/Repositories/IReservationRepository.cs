using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<Result<Reservation>> CreateAsync(Reservation reservation);
        Task<Result<Reservation>> UpdateAsync(UpdateReservationRequest reservation, int userId);
        Task<Result> DeleteAsync(int reservationId, int userId);
        Task<Result<List<Reservation>>> GetListAsync();
        Task<bool> UserHasReservationAsync(int userId, DateTime startTime, DateTime endTime, int? reservationId = null);

    }
}
