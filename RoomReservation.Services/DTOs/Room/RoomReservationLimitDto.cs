using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    /// <summary>
    /// Room reservation limits in minutes
    /// </summary>
    public class RoomReservationLimitDto
    {
        /// <summary>
        /// Minimum time of reservation
        /// </summary>
        /// <example>15</example>
        public int? MinTime { get; set; }
        /// <summary>
        /// Maximum time of reservation
        /// </summary>
        /// <example>90</example>
        public int? MaxTime { get; set; }
    }
}
