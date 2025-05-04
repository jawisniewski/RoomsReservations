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

    /// <summary>
    /// Filters for searching rooms
    /// </summary>
    public class RoomFilter
    {
        /// <summary>
        /// Count of tables
        /// </summary>
        /// <example>10</example>
        public int? TableCount { get; set; }
        /// <summary>
        /// Name of room
        /// </summary>
        /// <example> Sala konferencyjna 1</example>
        public string? Name { get; set; }
        /// <summary>
        /// Minimum capacity of room
        /// </summary>
        /// <example>5</example>
        public int? Capacity { get; set; }
        /// <summary>
        /// Layout of room: Boardroom, Theater, Classroom
        /// </summary>
        /// <remarks> /remarks>
        public RoomLayoutEnum? RoomLayout { get; set; }
        /// <summary>
        /// Room equipments: Projector, Whiteboard, VideoConference, Screen  
        /// </summary>
        public EquipmentTypeEnum[]? EquipmentTypes { get; set; }
    }
}
