using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<bool> AuthorizeAsync(string username, string password);
        public Task<int> GetUserIdAsync(string username);
    }
}
