using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.DTOs.Room.UpdateRoom;
using RoomReservation.Application.Interfaces.Services;

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
        await _roomService.CreateAsync(createRoom);

        return Ok();

    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateRoomRequest updateRoom)
    {
        await _roomService.UpdateAsync(updateRoom);

        return Ok();

    }

    [HttpDelete( "Delete")]
    public async Task<ActionResult> Delete(int roomId)
    {
        await _roomService.DeleteAsync(roomId);

        return Ok();

    }

    [HttpGet("GetByName")]
    public async Task<ActionResult<CreateRoomRequest>> GetByName(string name)
    {
        var room = await _roomService.GetByNameAsync(name);
        return Ok(room);
    }

    [HttpGet("GetList")]
    public async Task<ActionResult<List<CreateRoomRequest>>> GetList()
    {
        var rooms = await _roomService.GetListAsync();
        return Ok(rooms);
    }
}
