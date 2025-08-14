using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.PropertiesApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/rooms" )]
    [Produces( "application/json" )]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController( IRoomService roomService )
        {
            _roomService = roomService;
        }

        [HttpGet( "{id:int}", Name = "GetRoomById" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            RoomReadDto dto = await _roomService.Get( id, ct );

            return Ok( dto );
        }

        [HttpPut( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] RoomUpdateDto dto, CancellationToken ct )
        {
            await _roomService.Update( id, dto, ct );

            return NoContent();
        }

        [HttpDelete( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _roomService.Remove( id, ct );

            return NoContent();
        }
    }
}