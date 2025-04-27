using AutoMapper;
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
        public ReservationService(IMapper mapper, IReservationRepository reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }
        public async Task<Result<ReservationDto>> CreateAsync(CreateReservationRequest reservationRequest, int userId)
        {
            var reservation = _mapper.Map<Domain.Entities.Reservation>(reservationRequest);
            reservation.UserId = userId;
            var result = await _reservationRepository.CreateAsync(reservation);
            return _mapper.Map<Result<ReservationDto>>(result);
        }

        public async Task<Result<bool>> DeleteAsync(int reservationId, int userId)
        {
            return await _reservationRepository.DeleteAsync(reservationId, userId);
        }

        public async Task<Result<List<ReservationDto>>> GetListAsync()
        {
           var result = await _reservationRepository.GetListAsync();

            return _mapper.Map<Result<List<ReservationDto>>>(result);
        }

        public async Task<Result<ReservationDto>> UpdateAsync(UpdateReservationRequest updateReservationRequest, int userId)
        {
            var result = await _reservationRepository.UpdateAsync(updateReservationRequest, userId);
            return _mapper.Map<Result<ReservationDto>>(result);
        }
    }
}
