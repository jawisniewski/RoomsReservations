using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;

namespace RoomReservation.API.Validators.ReservationValidators
{
    public class ReservationValidator : BaseReservationValidator<ReservationDto>
    {
        public ReservationValidator() : base() { }
    }
}
