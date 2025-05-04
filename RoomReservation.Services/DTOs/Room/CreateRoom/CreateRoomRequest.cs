using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Room.CreateRoom
{

    /// <summary>
    /// Model for create room 
    /// </summary>
    public class CreateRoomRequest : RoomBaseDto
    {
        /// <summary>
        /// List of equipments, require at least one equipment 
        /// </summary>
        [Required]
        public required List<CreateRoomEquipmentRequest> RoomEquipments { get; set; }

    }
}
