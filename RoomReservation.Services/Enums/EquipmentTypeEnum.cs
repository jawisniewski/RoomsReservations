using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Enums
{
    /// <summary>
    /// Enum for equipment type
    /// </summary>
    public enum EquipmentTypeEnum
    {
        /// <summary>
        /// Projector equipment type
        /// </summary>
        Projector = 1,
        /// <summary>
        /// Whiteboard equipment type
        /// </summary>
        Whiteboard = 2,
        /// <summary>
        /// VideoConference equipment type
        /// </summary>
        /// <remarks> Video conference require projector or screen</remarks>
        VideoConference = 3,
        /// <summary>
        /// Screen equipment type
        /// </summary>
        Screen = 4
    }
}
