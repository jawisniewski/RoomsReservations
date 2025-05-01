using AutoMapper;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<ReservationService> _logger;
        public ReservationService(IMapper mapper, IReservationRepository reservationRepository, IRoomRepository roomRepository, ILogger<ReservationService> logger)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<Result> CreateAsync(CreateReservationRequest reservationRequest, int userId)
        {
            var reservation = _mapper.Map<Domain.Entities.Reservation>(reservationRequest);
            reservation.UserId = userId;

            var checkRoom = await _roomRepository.IsRoomAvailableAsync(reservationRequest.RoomId, reservationRequest.StartDate, reservationRequest.EndDate, null);

            if (!checkRoom.IsSuccess)
            {
                return checkRoom;
            }

            if (await _reservationRepository.UserHasReservationAsync(userId, reservationRequest.StartDate, reservationRequest.EndDate))
            {
                _logger.LogWarning($"User have already a reservation {userId}");
                return Result.Failure($"User {userId} have already a reservation", System.Net.HttpStatusCode.BadRequest);
            }

            var result = await _reservationRepository.CreateAsync(reservation);

            return _mapper.Map<Result<ReservationDto>>(result);
        }

        public async Task<Result> DeleteAsync(int reservationId, int userId)
        {
            return await _reservationRepository.DeleteAsync(reservationId, userId);
        }

        public async Task<Result<List<ReservationDto>>> GetListAsync()
        {
            var result = await _reservationRepository.GetListAsync();

            return _mapper.Map<Result<List<ReservationDto>>>(result);
        }

        public async Task<Result> UpdateAsync(UpdateReservationRequest updateReservationRequest, int userId)
        {
            var checkRoom = await _roomRepository.IsRoomAvailableAsync(updateReservationRequest.RoomId, updateReservationRequest.StartDate, updateReservationRequest.EndDate, updateReservationRequest.Id);

            if (!checkRoom.IsSuccess)
            {
                return checkRoom;
            }

            if (await _reservationRepository.UserHasReservationAsync(userId, updateReservationRequest.StartDate, updateReservationRequest.EndDate, updateReservationRequest.Id))
            {
                _logger.LogWarning($"User {userId} have already a reservation");

                return Result.Failure("User have already a reservation", System.Net.HttpStatusCode.BadRequest);
            }

            var result = await _reservationRepository.UpdateAsync(updateReservationRequest, userId);

            return _mapper.Map<Result<ReservationDto>>(result);
        }
    }
}
