using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room.CreateRoom
{
    public class CreateRoomRequest : RoomBaseDto
    {
        public required List<CreateRoomEquipmentRequest> RoomsEquipments { get; set; }

    }
}
