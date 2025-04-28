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
        public virtual Room? Room { get; set; }
        public virtual User? User { get; set; }
    }
}
