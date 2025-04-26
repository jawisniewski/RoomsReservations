using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.DTOs.Room.UpdateRoom;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Room> CreateAsync(CreateRoomRequest room);
        Task<RoomDto> UpdateAsync(UpdateRoomRequest room);
        Task<bool> DeleteAsync(int roomId);
        Task<RoomDto> GetByNameAsync(string name);
        Task<List<RoomDto>> GetListAsync();
    }
}
