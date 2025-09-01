using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.PropertiesApi
{
    /// <summary>
    /// Операции над комнатами.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/rooms" )]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController( IRoomService roomService )
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Получить комнату по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <returns>Объект комнаты или код 404, если не найдена.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_GetById", Summary = "Получить комнату по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            RoomReadDto dto = await _roomService.GetById( id );

            return Ok( dto );
        }

        /// <summary>
        /// Обновить комнату.
        /// </summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <param name="dto">Новые данные.</param>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_Update", Summary = "Обновить комнату" )]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] RoomUpdateDto dto )
        {
            await _roomService.Update( id, dto );

            return NoContent();
        }

        /// <summary>
        /// Удалить комнату.
        /// </summary>
        /// <param name="id">Идентификатор комнаты.</param>
        /// <returns>Код 204 без содержимого при успешном удалении; 404 если не найдена.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Rooms_Delete", Summary = "Удалить комнату" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _roomService.Remove( id );

            return NoContent();
        }
    }
}