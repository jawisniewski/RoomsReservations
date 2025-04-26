using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Entities
{
    public class RoomReservationLimit
    {
        public required int Id { get; set; }
        public required int RoomId { get; set; }
        public int MinTime{ get; set; }
        public int MaxTime{ get; set; }
        public Room? Room { get; set; }
    }
}
