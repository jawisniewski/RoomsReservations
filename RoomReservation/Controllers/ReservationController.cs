using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Controllers;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Shared.Common;
using System.Security.Claims;
using RoomReservation.API.UserClaims;
using RoomReservation.API.Helper;
using RoomReservation.Application.DTOs;
using RoomReservation.Application.DTOs.Reservation;

namespace RoomReservation.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationService _reservationService;

        public ReservationController(ILogger<ReservationController> logger, IReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest createReservationRequest)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userIdClaim, out var userId);

            var reservationResult = await _reservationService.CreateAsync(createReservationRequest, User.GetUserId());

            if(reservationResult.IsSuccess)
            {
                return  ((Result<ReservationDto>)reservationResult).ToActionResult();
            }

            return reservationResult.ToActionResult();
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateReservationRequest updateReservationRequest)
        {
            var updateReservationResult = await _reservationService.UpdateAsync(updateReservationRequest, User.GetUserId());

            if (updateReservationResult.IsSuccess)
            {
                return ((Result<ReservationDto>)updateReservationResult).ToActionResult();
            }

            return updateReservationResult.ToActionResult();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(BaseDeleteRequest deleteRequest)
        {
            var deleteResult = await _reservationService.DeleteAsync(deleteRequest.Id, User.GetUserId());

            return deleteResult.ToActionResult();
         
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var getByListResult = await _reservationService.GetListAsync();

            return getByListResult.ToActionResult();
           
        }

    }
}
