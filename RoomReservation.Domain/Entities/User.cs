﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Domain.Entities
{
   public class User
    {
        public required int Id { get; set; } 
        public required string Username { get; set; }
        public required string Password { get; set; }
        public List<Reservation>? Reservations { get; set; } = new List<Reservation>();
    }
}
