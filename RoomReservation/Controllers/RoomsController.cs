using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.API.Helper;
using RoomReservation.API.Validators;
using RoomReservation.Application.DTOs;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Shared.Common;
using System;

namespace RoomReservation.Controllers;
/// <summary>
/// Rooms managing
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly ILogger<RoomsController> _logger;
    private readonly IRoomService _roomService;

    public RoomsController(ILogger<RoomsController> logger, IRoomService roomService)
    {
        _logger = logger;
        _roomService = roomService;
    }
    /// <summary>
    /// Create a room
    /// </summary>
    /// <param name="createRoom"></param>
    /// <returns>created room with limits and equipments</returns>
    /// <response code="200">Room created</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Not found</response>
    /// <response code="422">Create room cannot be proceed </response>
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateRoomRequest createRoom)
    {
        var roomCreateResult = await _roomService.CreateAsync(createRoom);

        return roomCreateResult.ToActionResult();

    }

    /// <summary>
    /// Update a room
    /// </summary>
    /// <param name="updateRoom"></param>
    /// <returns>updated room with limits and equipments</returns>
    /// <response code="200">Room updated</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Not found</response>
    /// <response code="422">Update room cannot be proceed </response>
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] RoomDto updateRoom)
    {
        var roomResult = await _roomService.UpdateAsync(updateRoom);

        return roomResult.ToActionResult();

    }

    /// <summary>
    /// Delete a room
    /// </summary>
    /// <param name="deleteRequest"></param>
    /// <returns></returns>
    /// <response code="200">Room delete</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Not found</response>
    /// <response code="422">delete room cannot be proceed </response>
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery]BaseDeleteRequest deleteRequest)
    {
       var deleteResult = await _roomService.DeleteAsync(deleteRequest.Id);

        return deleteResult.ToActionResult();
    }

    /// <summary>
    /// Get list of room with equipments and limits
    /// </summary>
    /// <param name="roomFilters"></param>
    /// <returns></returns>
    /// <response code="200">Room list</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Not found</response>
    /// <response code="422">Get list cannot be proceed </response>
    [ProducesResponseType(typeof(List<RoomDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]
    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] RoomFilter roomFilters)
    {
        var getByListResult = await _roomService.GetListAsync(roomFilters);

        return getByListResult.ToActionResult();
    }

    /// <summary>
    /// Get list of room with equipments and limits in specific date range
    /// </summary>
    /// <param name="roomAvalibilityRequest"></param>
    /// <returns></returns>
    /// <response code="200">Available Room list</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Not found</response>
    /// <response code="422">Get Available rooms cannot be proceed </response>
    [ProducesResponseType(typeof(List<RoomDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status422UnprocessableEntity)]
    [HttpGet("GetAvalibityRooms")]
    public async Task<IActionResult> GetAvalibityRooms([FromQuery] RoomAvalibilityRequest roomAvalibilityRequest)
    {
        var getByListResult = await _roomService.GetAvalibilityRoomAsync(roomAvalibilityRequest);
        return getByListResult.ToActionResult();
    }
}
