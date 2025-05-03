using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Entities
{
    public class RoomEquipment
    {
        public required int Id { get; set; }
        public int Quantity { get; set; }
        public virtual Equipment? Equipment { get; set; }
        public virtual Room? Room { get; set; }
        public required int EquipmentId { get; set; }
        public required int RoomId { get; set; }

    }
}
