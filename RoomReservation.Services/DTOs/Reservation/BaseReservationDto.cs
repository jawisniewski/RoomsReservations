using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Reservation
{
    /// <summary>
    /// Base reservation model
    /// </summary>
    public class BaseReservationDto
    {
        private DateTime _startDate { get; set; }
        private DateTime _endDate { get; set; }

        /// <summary>
        /// Start of the reservation must be in the future 
        /// </summary>
        /// <example>2025.06.04T10:00</example>
        public required DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, DateTimeKind.Utc); }
        }

        /// <summary>
        /// End of the reservation must be in the future and after start date
        /// </summary>
        /// <example>2025.06.04T11:00</example>
        public required DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, DateTimeKind.Utc); }
        }
        /// <summary>
        /// Id of room
        /// </summary>
        /// <example>1</example>
        public required int RoomId { get; set; }
    }
}
