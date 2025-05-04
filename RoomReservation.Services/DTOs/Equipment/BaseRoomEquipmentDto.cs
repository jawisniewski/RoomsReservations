using RoomReservation.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Equipment
{
    /// <summary>
    /// Base room equipment model
    /// </summary>
    public class BaseRoomEquipmentDto
    {
        /// <summary>
        /// Equipment type
        /// </summary>
        /// <example>VideoConference</example>
        public required EquipmentTypeEnum EquipmentType { get; set; }
        /// <summary>
        /// Quantity of equipment
        /// </summary>
        /// <example> 3 </example>
        public int Quantity { get; set; }
    }
}
