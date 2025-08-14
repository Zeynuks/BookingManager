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
        private readonly IServiceService _serviceService;
        private readonly IAmenityService _amenityService;

        public RoomTypeController(
            IRoomTypeService roomTypeService,
            IRoomService roomService,
            IServiceService serviceService,
            IAmenityService amenityService )
        {
            _roomTypeService = roomTypeService;
            _roomService = roomService;
            _serviceService = serviceService;
            _amenityService = amenityService;
        }

        /// <summary>Получить тип комнаты по идентификатору.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "RoomTypes_GetById", Summary = "Получить тип комнаты по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( RoomTypeDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            RoomTypeDto dto = await _roomTypeService.Get( id, ct );

            return Ok( dto );
        }

        /// <summary>Обновить тип комнаты.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "RoomTypes_Update", Summary = "Обновить тип комнаты" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] RoomTypeUpdateDto dto, CancellationToken ct )
        {
            await _roomTypeService.Update( id, dto, ct );

            return NoContent();
        }

        /// <summary>Удалить тип комнаты.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "RoomTypes_Delete", Summary = "Удалить тип комнаты" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _roomTypeService.Remove( id, ct );

            return NoContent();
        }

        /// <summary>Получить комнаты заданного типа.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}/rooms" )]
        [SwaggerOperation( OperationId = "RoomTypes_GetRooms", Summary = "Список комнат этого типа" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<RoomReadDto> ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetRooms( int id, CancellationToken ct )
        {
            IReadOnlyList<RoomReadDto> getList = await _roomService.GetListByRoomTypeId( id, ct );

            return Ok( getList );
        }

        /// <summary>Создать комнату для заданного типа.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="dto">Данные для создания комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPost( "{id:int}/rooms" )]
        [SwaggerOperation( OperationId = "RoomTypes_CreateRoom", Summary = "Создать комнату выбранного типа" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( RoomReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> CreateRoom( int id, [FromBody] RoomCreateDto dto, CancellationToken ct )
        {
            int roomId = await _roomService.Create( id, dto, ct );
            RoomReadDto createdRoom = await _roomService.Get( roomId, ct );

            RouteValueDictionary routeValues = new()
            {
                { "id", roomId }
            };

            return CreatedAtAction( nameof( RoomController.GetById ), routeValues, createdRoom );
        }

        /// <summary>Получить сервисы, связанные с типом комнаты.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}/services" )]
        [SwaggerOperation( OperationId = "RoomTypes_GetServices", Summary = "Список сервисов для типа комнаты" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<ServiceReadDto> ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetServices( int id, CancellationToken ct )
        {
            IReadOnlyList<ServiceReadDto> getList = await _serviceService.GetListByRoomType( id, ct );

            return Ok( getList );
        }

        /// <summary>Получить удобства, связанные с типом комнаты.</summary>
        /// <param name="id">Идентификатор типа комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}/amenities" )]
        [SwaggerOperation( OperationId = "RoomTypes_GetAmenities", Summary = "Список удобств типа комнаты" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<AmenityReadDto> ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetAmenities( int id, CancellationToken ct )
        {
            IReadOnlyList<AmenityReadDto> getList = await _amenityService.GetListByRoomType( id, ct );

            return Ok( getList );
        }
    }
}