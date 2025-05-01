using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Reservation
{
    public class BaseReservationDto
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int RoomId { get; set; }
    }
}
