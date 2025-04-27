using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Shared.Common;
using RoomReservation.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public IHasher _hasher;
        public UserService(IUserRepository userRepository, IHasher hasher )
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }
        public async Task<bool> AuthorizeAsync(string username, string password)
        {
            return await _userRepository.IsUserAuthorizedAsync(username, _hasher.Hash(password));
        }

        public async Task<int> GetUserIdAsync(string username)
        {
            return await _userRepository.GetUserIdAsync(username);
        }
    }
}
