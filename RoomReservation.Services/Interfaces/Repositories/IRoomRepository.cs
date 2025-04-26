using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> CreateAsync(Room room);
        Task<Room> UpdateAsync(Room room);
        Task<bool> DeleteAsync(int roomId);
        Task<Room> GetByNameAsync(string name);
        Task<List<Room>> GetListAsync();
    }
}
