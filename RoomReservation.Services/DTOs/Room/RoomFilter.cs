using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    public class RoomFilter
    {
        public int? TableCount { get; set; }
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public RoomLayoutEnum? RoomLayout { get; set; }
        public int[]? EquipmentIds { get; set; }
    }
}
