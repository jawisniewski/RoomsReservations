using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    public class RoomReservationLimitDto
    {
        public int Id { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }
    }
}
