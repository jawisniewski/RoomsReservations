using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room.UpdateRoom
{
    public class UpdateRoomRequest
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public required RoomLayoutEnum RoomLayout { get; set; }
        public  List<RoomEquipmentRequest>? RoomsEquipments { get; set; }
        public CreateRoomReservationLimitRequest? RoomReservationLimit { get; set; }
    }
}
