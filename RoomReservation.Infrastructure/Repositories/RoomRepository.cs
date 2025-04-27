using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Domain.Entities;
using RoomReservation.Infrastructure.Context;
using RoomReservation.Shared.Common;
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

        public async Task<Result<Room>> CreateAsync(Room room)
        {
            await _dbSet.AddAsync(room);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to create room");

                return Result<Room>.Failure("Failed to create room");
            }

            return Result<Room>.Success(room);
        }

        public async Task<Result<bool>> DeleteAsync(int roomId)
        {
            var room = await _dbSet.FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                _logger.LogError($"Room not found ${roomId}");


                return Result<bool>.Failure($"Room not found ${roomId}");
            }

            _dbSet.Remove(room);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError($"Failed to delete room ${roomId}");

                return Result<bool>.Failure($"Failed to delete room ${roomId}");
            }

            return Result<bool>.Success();
        }

        public async Task<Result<Room>> GetByNameAsync(string name)
        {
            var room = await _dbSet.Where(x => x.Name.ToLower() == name.ToLower())
                 .Include(ri => ri.RoomsEquipments)
                     .ThenInclude(i => i.Equipment)
                 .Include(x => x.RoomReservationLimit)
             .FirstOrDefaultAsync();

            if (room == null)
            {
                _logger.LogError($"Room not found {name}");
                return Result<Room>.Failure($"Room not found {name}");
            }

            return Result<Room>.Success(room);
        }

        public async Task<Result<List<Room>>> GetListAsync()
        {
            var rooms = await _dbSet
                .Include(ri => ri.RoomsEquipments)
                    .ThenInclude(i => i.Equipment)
                .Include(x => x.RoomReservationLimit)
                .ToListAsync();

            if (rooms == null)
            {
                _logger.LogError("Failed to get rooms");
                return Result<List<Room>>.Failure("Failed to get rooms");
            }

            if (rooms.Count == 0)
            {
                _logger.LogError("No rooms found");
                return Result<List<Room>>.Failure("No rooms found");
            }

            return Result<List<Room>>.Success(rooms);
        }

        public async Task<Result<Room>> UpdateAsync(Room room)
        {
            _dbSet.Update(room);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("Failed to update room");
                return Result<Room>.Failure("Failed to update room");
            }

            return Result<Room>.Success(room);
        }
    }
}
