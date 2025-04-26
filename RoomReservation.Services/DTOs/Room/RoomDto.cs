using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    public class RoomDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public required RoomLayoutEnum RoomLayout { get; set; }
        public List<RoomEquipmentDto>? RoomsEquipments { get; set; }
        public CreateRoomReservationLimitRequest? RoomReservationLimit { get; set; }
    }
}
