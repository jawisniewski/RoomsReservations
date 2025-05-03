using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Enums
{
    /// <summary>
    /// Room layouts
    /// </summary>
    public enum RoomLayoutEnum
    {
        /// <summary>
        /// Boardroom layout
        /// </summary>
        /// <remarks> Require at least 1 table</remarks>
        Boardroom,
        /// <summary>
        /// Theater layout
        /// </summary>
        /// <remarks>Cannot have table/remarks>
        Theater,
        /// <summary>
        /// Classroom
        /// </summary>
        /// <remarks> Require at least 2 tables and capacity must be even</remarks>
        Classroom
    }
}
