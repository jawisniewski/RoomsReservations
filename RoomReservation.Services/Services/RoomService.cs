using AutoMapper;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Domain.Entities;
using RoomReservation.Shared.Common;
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

        public async Task<Result<RoomDto>> CreateAsync(CreateRoomRequest room)
        {
            var roomEntity = _mapper.Map<Room>(room);
            var createRoomResult = await _roomRepository.CreateAsync(roomEntity);


            return _mapper.Map<Result<RoomDto>>(createRoomResult);
        }

        public async Task<Result> DeleteAsync(int roomId)
        {
            var deleteResult = await _roomRepository.DeleteAsync(roomId);

            return deleteResult;
        }

        public async Task<Result<RoomDto>> UpdateAsync(RoomDto updateRoom)
        {
            var roomResult = await _roomRepository.UpdateAsync(updateRoom);

            return _mapper.Map<Result<RoomDto>>(roomResult);
        }

        public async Task<Result<List<RoomDto>>> GetListAsync(RoomFilter roomFilter)
        {
            var roomsResult = await _roomRepository.GetListAsync(roomFilter);

            return _mapper.Map<Result<List<RoomDto>>>(roomsResult);
        }
        public async Task<Result<List<RoomDto>>> GetAvalibilityRoomAsync(RoomAvalibilityRequest roomAvalibilityRequest)
        {
            var roomsResult = await _roomRepository.GetAvalibilityRoomAsync(roomAvalibilityRequest);

            return _mapper.Map<Result<List<RoomDto>>>(roomsResult);

        }
    }
}