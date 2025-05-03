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
        /// Available from must be in the future
        /// </summary>
        [Required]
        public DateTime AvailableFrom { get; set; }
        /// <summary>
        /// Available to must be in the future and after available from
        /// </summary>
        [Required]
        public DateTime AvailableTo { get; set; }
    }
}
