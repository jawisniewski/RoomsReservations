using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs
{
    public class EquipmentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
