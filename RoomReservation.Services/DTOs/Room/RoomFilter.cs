using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Application.Enums;
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
        public EquipmentTypeEnum[]? EquipmentTypes { get; set; }
    }
}
