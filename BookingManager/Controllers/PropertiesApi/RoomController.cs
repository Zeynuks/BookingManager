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
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Объект комнаты или код 404, если не найдена.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_GetById", Summary = "Получить комнату по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( RoomReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken cancellationToken )
        {
            RoomReadDto dto = await _roomService.GetById( id, cancellationToken );

            return Ok( dto );
        }

        /// <summary>Обновить комнату.</summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_Update", Summary = "Обновить комнату" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] RoomUpdateDto dto,
            CancellationToken cancellationToken )
        {
            await _roomService.Update( id, dto, cancellationToken );

            return NoContent();
        }

        /// <summary>Удалить комнату.</summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном удалении; 404 если не найдена.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_Delete", Summary = "Удалить комнату" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken cancellationToken )
        {
            await _roomService.Remove( id, cancellationToken );

            return NoContent();
        }
    }
}