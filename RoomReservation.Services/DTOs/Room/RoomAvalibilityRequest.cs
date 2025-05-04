using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room
{
    /// <summary>
    /// Request for room avalibility
    /// </summary>
    public class RoomAvalibilityRequest : RoomFilter
    {
        /// <summary>
        /// Available from must be in the future MM.DD.YYYY HH:mm
        /// </summary>
        /// <example>10.07.2025 15:00</example>
        [Required]
        public DateTime AvailableFrom { get; set; }
        /// <summary>
        /// Available to must be in the future and after available from  MM.DD.YYYY HH:mm
        /// </summary>
        /// <example>10.07.2025 16:00</example>
        [Required]
        public DateTime AvailableTo { get; set; }
    }
}
