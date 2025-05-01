using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Domain.Entities;
using RoomReservation.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _dbSet = context.Set<User>();
            _logger = logger;

        }

        public async Task<int> GetUserIdAsync(string username)
        {
            return await _dbSet.Where(u => u.Username == username).Select(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserAuthorizedAsync(string username, string password)
        {
            return await _dbSet.AnyAsync(x => x.Username == username && x.Password == password);

        }
    }
}
