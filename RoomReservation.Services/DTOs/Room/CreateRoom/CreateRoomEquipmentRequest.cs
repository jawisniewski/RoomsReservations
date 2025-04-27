using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room.CreateRoom
{
    public class CreateRoomEquipmentRequest
    {
        public required int EquipmentId { get; set; }
        public int Quantity { get; set; }
    }
}
