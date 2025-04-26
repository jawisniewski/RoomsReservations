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
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Room> _dbSet;
        private readonly ILogger<RoomRepository> _logger;
        public RoomRepository(AppDbContext context, ILogger<RoomRepository> logger)
        {
            _context = context;
            _dbSet = context.Set<Room>();
            _logger = logger;

        }

        public async Task<Room> CreateAsync(Room room)
        {
            await _dbSet.AddAsync(room);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to create room");

                return null;
            }

            return room;
        }

        public async Task<bool> DeleteAsync(int roomId)
        {
            var room = await _dbSet.FirstOrDefaultAsync(r => r.Id == roomId);

            if(room == null)
            {
                _logger.LogError($"Room not found ${roomId}");

                return false;
            }

            _dbSet.Remove(room);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError($"Failed to delete room ${roomId}");

                return false;
            }
            return true;
        }

        public async Task<Room> GetByNameAsync(string name)
        {
            var room = await _dbSet.Where(x => x.Name == name)
                 .Include(ri => ri.RoomsEquipments)
                     .ThenInclude(i => i.Equipment)
                 .Include(x => x.RoomReservationLimit)
             .FirstOrDefaultAsync();

            return room;
        }

        public Task<List<Room>> GetListAsync()
        {
            var rooms = _dbSet
                .Include(ri => ri.RoomsEquipments)
                    .ThenInclude(i => i.Equipment)
                .Include(x => x.RoomReservationLimit)
                .ToListAsync();

            return rooms;
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            _dbSet.Update(room);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to update room");
                return null;
            }

            return room;
        }
    }
}
