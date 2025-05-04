using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs
{
    /// <summary>
    /// Delete model request
    /// </summary>
    public class BaseDeleteRequest
    {
        /// <summary>
        /// Id of element to delete, must be positive number
        /// </summary>
        /// <example>1</example>
        [Required]

        public int Id { get; set; }
    }
}
