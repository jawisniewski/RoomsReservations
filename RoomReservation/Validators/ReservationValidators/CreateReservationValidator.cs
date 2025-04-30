using RoomReservation.API.Validators.Room;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Room.CreateRoom;

namespace RoomReservation.API.Validators.ReservationValidators
{
    public class CreateReservationValidator : BaseReservationValidator<CreateReservationRequest>
    {
        public CreateReservationValidator() : base() { }
    }
}
