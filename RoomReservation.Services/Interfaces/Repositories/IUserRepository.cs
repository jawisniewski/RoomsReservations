using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsUserAuthorizedAsync(string username, string password);
        Task<int> GetUserIdAsync(string username);
    }
}
