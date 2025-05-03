using RoomReservation.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Equipment
{
    public class BaseRoomEquipmentDto
    {
        public required EquipmentTypeEnum EquipmentType { get; set; }
        public int Quantity { get; set; }
    }
}
