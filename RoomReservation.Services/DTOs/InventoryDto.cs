using RoomReservation.Application.DTOs.Room.CreateRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs
{
    public class InventoryDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public List<CreateRoomEquipmentRequest>? RoomsInventories { get; set; }
    }
}
