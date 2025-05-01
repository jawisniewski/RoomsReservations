using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    public class RoomBaseDto
    {
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public required int TableCount { get; set; }
        public required RoomLayoutEnum RoomLayout { get; set; }
        public required List<RoomEquipmentDto> RoomsEquipments { get; set; }
        public RoomReservationLimitDto? RoomReservationLimit { get; set; }
    }
}
