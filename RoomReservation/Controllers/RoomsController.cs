using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.API.Helper;
using RoomReservation.API.Validators;
using RoomReservation.Application.DTOs;
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
    public async Task<IActionResult> Create([FromBody] CreateRoomRequest createRoom)
    {
        var roomCreateResult = await _roomService.CreateAsync(createRoom);

        return roomCreateResult.ToActionResult();

    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromBody] RoomDto updateRoom)
    {
        var roomResult = await _roomService.UpdateAsync(updateRoom);

        return roomResult.ToActionResult();

    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(BaseDeleteRequest deleteRequest)
    {
       var deleteResult = await _roomService.DeleteAsync(deleteRequest.Id);

        return deleteResult.ToActionResult();
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] RoomFilter roomFilters)
    {
        var getByListResult = await _roomService.GetListAsync(roomFilters);

        return getByListResult.ToActionResult();
    }

    /// <summary>
    /// Get Avalibility Rooms
    /// </summary>
    /// <param name="roomAvalibilityRequest"></param>
    /// <returns></returns>
    [HttpGet("GetAvalibityRooms")]
    public async Task<IActionResult> GetAvalibityRooms([FromQuery] RoomAvalibilityRequest roomAvalibilityRequest)
    {
        var getByListResult = await _roomService.GetAvalibilityRoomAsync(roomAvalibilityRequest);
        return getByListResult.ToActionResult();
    }
}
