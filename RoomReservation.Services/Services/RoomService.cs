using AutoMapper;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.DTOs.Room.UpdateRoom;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Services
{
    public class RoomService : IRoomService
    {
        public ILogger<RoomService> _logger;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomService(ILogger<RoomService> logger, IRoomRepository roomRepository, IMapper mapper)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _mapper = mapper;

        }

        public async Task<Room> CreateAsync(CreateRoomRequest room)
        {
            var roomEntity = _mapper.Map<Domain.Entities.Room>(room);
            var createRoomResult = await _roomRepository.CreateAsync(roomEntity);

            if (createRoomResult == null)
            {
                _logger.LogError($"Failed to create room {room.Name}");

                return null;
            }

            _logger.LogInformation($"Room created successfully {room.Name}");

            return createRoomResult;
        }

        public async Task<bool> DeleteAsync(int roomId)
        {
            var deleteRusult = await _roomRepository.DeleteAsync(roomId);

            return deleteRusult;
        }

        public async Task<RoomDto> GetByNameAsync(string name)
        {
            var room = await _roomRepository.GetByNameAsync(name);

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<List<RoomDto>> GetListAsync()
        {
            var rooms = await _roomRepository.GetListAsync();
            return _mapper.Map<List<RoomDto>>(rooms);
        }

        public async Task<RoomDto> UpdateAsync(UpdateRoomRequest updateRoom)
        {
            var room =  await _roomRepository.UpdateAsync(_mapper.Map<Room>(updateRoom));

            return _mapper.Map<RoomDto>(room);
        }
    }
}