using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Entities
{
    public class RoomsEquipments
    {
        public required int Id { get; set; }
        public int Quantity { get; set; }
        public Equipment? Equipment { get; set; }
        public Room? Room { get; set; }
        public required int EquipmentId { get; set; }
        public required int RoomId { get; set; }

    }
}
