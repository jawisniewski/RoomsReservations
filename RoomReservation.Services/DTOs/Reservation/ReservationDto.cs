using RoomReservation.Application.DTOs.Room;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Reservation
{
    /// <summary>
    /// Reservation model
    /// </summary>
    public class ReservationDto : BaseReservationDto
    {
        /// <summary>
        /// Reservation id
        /// </summary>
        /// <example> 1 </example>
        public required int Id { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        /// <example> 1 </example>
        public required int UserId { get; set; }
        /// <summary>
        /// Reservation room>
        /// </summary>
        public RoomDto? Room { get; set; }
    }
}
