using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    /// <summary>
    /// Base room model
    /// </summary>
    public class RoomBaseDto
    {
        /// <summary>
        /// Name of room
        /// </summary>
        /// <example>Sala konferencyjna 1</example>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// Capacity
        /// </summary>
        /// <example>15</example>
        [Required]
        public required int Capacity { get; set; }

        /// <summary>
        /// Table count
        /// </summary>
        /// <example> 5 </example>
        [Required]
        public required int TableCount { get; set; }

        /// <summary>
        /// Layout of room: Boardroom, Theater or Classroom
        /// </summary>
        /// <example>Classroom</example>
        [Required]
        public required RoomLayoutEnum RoomLayout { get; set; }

        /// <summary>
        /// Room reservation limits 
        /// </summary>
        public RoomReservationLimitDto? RoomReservationLimit { get; set; }
    }
}
