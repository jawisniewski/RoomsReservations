﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Shared.Interface
{
    public interface IHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
