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
    /// <summary>
    /// Reservations managing 
    /// </summary>
    /// <remarks>require authorization</remarks>
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
        /// <summary>
        /// Create a reservation
        /// </summary>
        /// <param name="createReservationRequest"></param>
        /// <returns>Created reservation with room</returns>
        /// <remarks></remarks>
        /// <response code="200">Reservation created</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">Room not found</response>
        /// <response code="422">Reservation cannot be proceed </response>
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]

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

        /// <summary>
        /// Update a reservation
        /// </summary>
        /// <param name="updateReservationRequest"></param>
        /// <returns>Updated reservation with room</returns>
        /// <response code="200">Reservation updated</response>
        /// <response code="400">Validation error</response>
        /// <response code="403">User is forbidden</response>
        /// <response code="404">Room not found</response>
        /// <response code="422">Reservation cannot be proceed </response>
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]
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

        /// <summary>
        /// Delete a reservation
        /// </summary>
        /// <param name="deleteRequest"></param>
        /// <returns></returns>
        /// <response code="200">Reservation deleted</response>
        /// <response code="400">Validation error</response>
        /// <response code="403">User is forbidden</response>
        /// <response code="422">Reservation cannot be proceed </response>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery]BaseDeleteRequest deleteRequest)
        {
            var deleteResult = await _reservationService.DeleteAsync(deleteRequest.Id, User.GetUserId());

            return deleteResult.ToActionResult();
         
        }
        /// <summary>
        /// Get list of reservation
        /// </summary>
        /// <returns>List of reservation with room</returns>
        /// <response code="200">List of reservations with rooms</response>
        /// <response code="400">Get list error</response>
        [ProducesResponseType(typeof(List<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var getByListResult = await _reservationService.GetListAsync();

            return getByListResult.ToActionResult();
           
        }

    }
}
