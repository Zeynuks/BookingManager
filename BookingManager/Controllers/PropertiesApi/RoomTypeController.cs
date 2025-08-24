using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.PropertiesApi
{
    /// <summary>Операции над типами комнат.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/room-types" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "RoomTypes" )]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRoomService _roomService;

        public RoomTypeController(
            IRoomTypeService roomTypeService,
            IRoomService roomService )
        {
            _roomTypeService = roomTypeService;
            _roomService = roomService;
        }

        /// <summary>Получить тип комнаты по идентификатору.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Тип комнаты или код 404, если не найден.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "RoomTypes_GetById", Summary = "Получить тип комнаты по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( RoomTypeDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken cancellationToken )
        {
            RoomTypeDto dto = await _roomTypeService.GetById( id, cancellationToken );

            return Ok( dto );
        }

        /// <summary>Обновить тип комнаты.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "RoomTypes_Update", Summary = "Обновить тип комнаты" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] RoomTypeUpdateDto dto,
            CancellationToken cancellationToken )
        {
            await _roomTypeService.Update( id, dto, cancellationToken );

            return NoContent();
        }

        /// <summary>Удалить тип комнаты.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном удалении; 404 если не найден.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "RoomTypes_Delete", Summary = "Удалить тип комнаты" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken cancellationToken )
        {
            await _roomTypeService.Remove( id, cancellationToken );

            return NoContent();
        }

        /// <summary>Получить комнаты заданного типа.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список комнат указанного типа.</returns>
        [HttpGet( "{id:int}/rooms" )]
        [SwaggerOperation( OperationId = "RoomTypes_GetRooms", Summary = "Список комнат этого типа" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<RoomReadDto> ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetRooms( int id, CancellationToken cancellationToken )
        {
            IReadOnlyList<RoomReadDto> getList = await _roomService.GetListByRoomTypeId( id, cancellationToken );

            return Ok( getList );
        }

        /// <summary>Создать комнату для заданного типа.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="dto">Данные для создания комнаты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданная комната с кодом 201 и заголовком Location.</returns>
        [HttpPost( "{id:int}/rooms" )]
        [SwaggerOperation( OperationId = "RoomTypes_CreateRoom", Summary = "Создать комнату выбранного типа" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( RoomReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> CreateRoom(
            int id,
            [FromBody] RoomCreateDto dto,
            CancellationToken cancellationToken )
        {
            int roomId = await _roomService.Create( id, dto, cancellationToken );
            RoomReadDto createdRoom = await _roomService.GetById( roomId, cancellationToken );

            RouteValueDictionary routeValues = new()
            {
                { "id", roomId }
            };

            return CreatedAtAction( nameof( RoomController.GetById ), routeValues, createdRoom );
        }
    }
}