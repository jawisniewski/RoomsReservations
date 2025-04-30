using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Application.DTOs.Reservation;

namespace RoomReservation.Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Result> CreateAsync(CreateReservationRequest room, int userId);
        Task<Result<ReservationDto>> UpdateAsync(UpdateReservationRequest room, int userId);
        Task<Result<bool>> DeleteAsync(int roomId, int userId);
        Task<Result<List<ReservationDto>>> GetListAsync();
    }
}
