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
        public async Task<ActionResult> Create([FromBody] CreateReservationRequest createReservationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userIdClaim, out var userId);

            var reservationResult = await _reservationService.CreateAsync(createReservationRequest, User.GetUserId());

            if (!reservationResult.IsSuccess)
                return UnprocessableEntity(reservationResult);

            return Ok(reservationResult);

        }

        [HttpPatch("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateReservationRequest updateReservationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roomResult = await _reservationService.UpdateAsync(updateReservationRequest, User.GetUserId());

            if (!roomResult.IsSuccess)
                return UnprocessableEntity(roomResult);

            return Ok(roomResult);

        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int roomId)
        {


            var deleteResult = await _reservationService.DeleteAsync(roomId, User.GetUserId());

            if (!deleteResult.IsSuccess)
                return UnprocessableEntity(deleteResult);

            return Ok();

        }

        [HttpGet("GetList")]
        public async Task<ActionResult<List<CreateRoomRequest>>> GetList()
        {
            var getByListResult = await _reservationService.GetListAsync();

            if (!getByListResult.IsSuccess)
                return UnprocessableEntity(getByListResult);

            return Ok(getByListResult);
        }

    }
}
