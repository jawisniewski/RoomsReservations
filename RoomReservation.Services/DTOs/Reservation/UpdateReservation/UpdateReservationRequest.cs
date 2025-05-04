using RoomReservation.Application.DTOs.Room;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Reservation.UpdateReservation
{
    /// <summary>
    /// Model for update reservation
    /// </summary>
    public class UpdateReservationRequest: BaseReservationDto
    {
        /// <summary>
        /// Id of the reservation
        /// </summary>
        /// <example>1</example>
        public required int Id { get; set; }
    }
}
