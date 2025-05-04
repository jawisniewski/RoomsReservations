using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Equipment
{
    /// <summary>
    /// Equipment model
    /// </summary>
    public class EquipmentDto
    {
        /// <summary>
        /// Equipment id
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Equipment name
        /// </summary>
        /// <example>Projektor</example>
        public required string Name { get; set; }
    }
}
