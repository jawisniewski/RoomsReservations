using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    public class RoomDto : RoomBaseDto
    {
        public required int Id { get; set; }
        public required List<RoomEquipmentDto> RoomsEquipments { get; set; }
    }
}
