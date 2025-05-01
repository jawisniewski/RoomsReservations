using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Domain.Entities;
using RoomReservation.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<Result<Room>> CreateAsync(Room room);
        Task<Result<Room>> UpdateAsync(RoomDto room);
        Task<Result> DeleteAsync(int roomId);
        Task<Result<List<Room>>> GetListAsync(RoomFilter roomFilter);
        Task<Result<List<Room>>> GetAvalibilityRoomAsync(RoomAvalibilityRequest roomAvalibilityRequest);
        Task<Result> IsRoomAvailableAsync(int roomId, DateTime startDate, DateTime endDate, int? reservationId);
    }
}
