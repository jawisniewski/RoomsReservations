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
    /// Room model
    /// </summary>
    public class RoomDto : RoomBaseDto
    {
        /// <summary>
        /// Room id 
        /// </summary>
        /// <example>1</example>
        [Required]
        public required int Id { get; set; }
        /// <summary>
        /// Room equipments, require minimum one of equipment
        /// </summary>
        [Required]
        public required List<RoomEquipmentDto> RoomEquipments { get; set; }
    }
}
