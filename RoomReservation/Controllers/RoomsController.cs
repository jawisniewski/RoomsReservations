using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
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
        var roomResult = await _roomService.CreateAsync(createRoom);
        if (!roomResult.IsSuccess)
            return UnprocessableEntity(roomResult);

        return Ok(roomResult);

    }

    [HttpPut("Update")]
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

    [HttpGet("GetByName")]
    public async Task<ActionResult<CreateRoomRequest>> GetByName(string name)
    {
        var getByNameResult = await _roomService.GetByNameAsync(name);

        if (!getByNameResult.IsSuccess)
            return UnprocessableEntity(getByNameResult);

        return Ok(getByNameResult);
    }

    [HttpGet("GetList")]
    public async Task<ActionResult<List<CreateRoomRequest>>> GetList()
    {
        var getByListResult = await _roomService.GetListAsync();        

        if (!getByListResult.IsSuccess)
            return UnprocessableEntity(getByListResult);

        return Ok(getByListResult);
    }
}
