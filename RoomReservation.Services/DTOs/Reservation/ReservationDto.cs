using RoomReservation.Application.DTOs.Room;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Reservation
{
    public class ReservationDto : BaseReservationDto
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public RoomDto? Room { get; set; }
    }
}
