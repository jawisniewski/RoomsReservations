using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.API.Validators;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Interfaces.Services;
using System;

namespace RoomReservation.Controllers;

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

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateRoomRequest createRoom)
    {
        var roomResult = await _roomService.CreateAsync(createRoom);
        if (!roomResult.IsSuccess)
            return UnprocessableEntity(roomResult);

        return Ok(roomResult);

    }

    [HttpPatch("Update")]
    public async Task<ActionResult> Update([FromBody] RoomDto updateRoom)
    {
        var roomResult = await _roomService.UpdateAsync(updateRoom);

        if (!roomResult.IsSuccess)
            return UnprocessableEntity(roomResult);

        return Ok(roomResult);

    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(int roomId)
    {
       var deleteResult = await _roomService.DeleteAsync(roomId);

        if (!deleteResult.IsSuccess)
            return UnprocessableEntity(deleteResult);

        return Ok();

    }

    [HttpGet("GetList")]
    public async Task<ActionResult<List<RoomDto>>> GetList([FromQuery] RoomFilter roomFilters)
    {
        var getByListResult = await _roomService.GetListAsync(roomFilters);        

        if (!getByListResult.IsSuccess)
            return UnprocessableEntity(getByListResult);

        return Ok(getByListResult);
    }

    /// <summary>
    /// Get Avalibility Rooms
    /// </summary>
    /// <param name="roomAvalibilityRequest"></param>
    /// <returns></returns>
    [HttpGet("GetAvalibityRooms")]
    public async Task<ActionResult<List<RoomDto>>> GetAvalibityRooms([FromQuery] RoomAvalibilityRequest roomAvalibilityRequest)
    {
        var getByListResult = await _roomService.GetAvalibilityRoomAsync(roomAvalibilityRequest);

        if (!getByListResult.IsSuccess)
            return UnprocessableEntity(getByListResult);

        return Ok(getByListResult);
    }
}
