using RoomReservation.Application.DTOs.Room;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Reservation.UpdateReservation
{
    public class UpdateReservationRequest: BaseReservationDto
    {
        public required int Id { get; set; }
    }
}
