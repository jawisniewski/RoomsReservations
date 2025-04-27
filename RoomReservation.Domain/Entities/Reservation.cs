using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Entities
{
    public class Reservation
    {
        public required int Id { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int RoomId { get; set; }
        public required int UserId { get; set; }
        public Room? Room { get; set; }
        public Users? User { get; set; }
        public required RoomLayoutEnum RoomLayout { get; set; }

    }
}
