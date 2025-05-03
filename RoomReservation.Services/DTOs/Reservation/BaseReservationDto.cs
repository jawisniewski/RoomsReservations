using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.DTOs.Reservation
{
    public class BaseReservationDto
    {
        private DateTime _startDate { get; set; }
        private DateTime _endDate { get; set; }

        public required DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, DateTimeKind.Utc); }
        }

        public required DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, DateTimeKind.Utc); }
        }

        public required int RoomId { get; set; }
    }
}
