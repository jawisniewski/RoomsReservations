using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room.CreateRoom
{
    public class CreateRoomReservationLimitRequest
    {
        public int MinTime { get; set; }
        public int MaxTime { get; set; }
    }
}
