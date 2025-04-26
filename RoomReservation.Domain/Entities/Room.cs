using RoomReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Entities
{
    public class Room
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public required RoomLayoutEnum RoomLayout { get; set; }
        public virtual List<Reservation>? Reservations { get; set; }
        public virtual RoomReservationLimit? RoomReservationLimit { get; set; }
        public virtual List<RoomsEquipments>? RoomsEquipments { get; set; }
    }
}
