using RoomReservation.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Shared.Common
{
    public class Hasher : IHasher
    {
        public string Hash(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public bool Verify(string password, string hash)
        {
            var hashedPassword = Hash(password);
            return hashedPassword == hash;
        }
    }
}
