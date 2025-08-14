using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.PropertiesApi
{
    /// <summary>Операции над комнатами.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/rooms" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "Rooms" )]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController( IRoomService roomService )
        {
            _roomService = roomService;
        }

        /// <summary>Получить комнату по идентификатору.</summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_GetById", Summary = "Получить комнату по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( RoomReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            RoomReadDto dto = await _roomService.Get( id, ct );

            return Ok( dto );
        }

        /// <summary>Обновить комнату.</summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_Update", Summary = "Обновить комнату" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] RoomUpdateDto dto, CancellationToken ct )
        {
            await _roomService.Update( id, dto, ct );

            return NoContent();
        }

        /// <summary>Удалить комнату.</summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_Delete", Summary = "Удалить комнату" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _roomService.Remove( id, ct );

            return NoContent();
        }
    }
}