using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Domain.Entities;
using RoomReservation.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Result<RoomDto>> CreateAsync(CreateRoomRequest room);
        Task<Result<RoomDto>> UpdateAsync(RoomDto room);
        Task<Result<bool>> DeleteAsync(int roomId);
        Task<Result<RoomDto>> GetByNameAsync(string name);
        Task<Result<List<RoomDto>>> GetListAsync();
    }
}
