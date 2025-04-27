using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Entities
{
    public class Equipment
    {
        public int Id { get; set; } 
        public required string Name { get; set; }
        public List<RoomsEquipments>? RoomsEquipments { get; set; }
    }
}
