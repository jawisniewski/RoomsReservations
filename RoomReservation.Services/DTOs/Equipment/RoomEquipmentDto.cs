using RoomReservation.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Equipment
{
    /// <summary>
    /// Room equipment model
    /// </summary>
    public class RoomEquipmentDto : BaseRoomEquipmentDto
    {
        /// <summary>
        /// Equipment id
        /// </summary>
        /// <example>1</example>
        public int? Id { get; set; }
    }
}
