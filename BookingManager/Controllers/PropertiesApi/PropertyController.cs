using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.PropertiesApi
{
    /// <summary>Операции над отелями.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/properties" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "Properties" )]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IRoomTypeService _roomTypeService;

        public PropertyController( IPropertyService propertyService, IRoomTypeService roomTypeService )
        {
            _propertyService = propertyService;
            _roomTypeService = roomTypeService;
        }

        /// <summary>Создать отель.</summary>
        /// <param name="dto">Данные для создания.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPost]
        [SwaggerOperation( OperationId = "Properties_Create", Summary = "Создать отель" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( PropertyReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] PropertyCreateDto dto, CancellationToken ct )
        {
            int id = await _propertyService.Create( dto, ct );
            PropertyReadDto createdProperty = await _propertyService.Get( id, ct );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, createdProperty );
        }

        /// <summary>Получить список отелей.</summary>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet]
        [SwaggerOperation( OperationId = "Properties_GetList", Summary = "Список отелей" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<PropertyReadDto> ) )]
        public async Task<IActionResult> GetList( CancellationToken ct )
        {
            IReadOnlyList<PropertyReadDto> getList = await _propertyService.GetList( ct );

            return Ok( getList );
        }

        /// <summary>Получить отель по идентификатору.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Properties_GetById", Summary = "Получить отель по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( PropertyReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            PropertyReadDto dto = await _propertyService.Get( id, ct );

            return Ok( dto );
        }

        /// <summary>Обновить отель.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Properties_Update", Summary = "Обновить отель" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] PropertyUpdateDto dto, CancellationToken ct )
        {
            await _propertyService.Update( id, dto, ct );

            return NoContent();
        }

        /// <summary>Удалить отель.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Properties_Delete", Summary = "Удалить отель" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _propertyService.Remove( id, ct );

            return NoContent();
        }

        /// <summary>Получить типы комнат для отеля.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}/room-types" )]
        [SwaggerOperation( OperationId = "Properties_GetRoomTypes", Summary = "Типы комнат отеля" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<RoomTypeDto> ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetRoomTypes( int id, CancellationToken ct )
        {
            IReadOnlyList<RoomTypeDto> getList = await _roomTypeService.GetListByProperty( id, ct );

            return Ok( getList );
        }

        /// <summary>Создать тип комнаты в отеле.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="dto">Данные для создания типа комнаты.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPost( "{id:int}/room-types" )]
        [SwaggerOperation( OperationId = "Properties_CreateRoomType", Summary = "Создать тип комнаты в отеле" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( RoomTypeDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> CreateRoomType( int id, [FromBody] RoomTypeCreateDto dto,
            CancellationToken ct )
        {
            int roomTypeId = await _roomTypeService.Create( id, dto, ct );
            RoomTypeDto createdRoomType = await _roomTypeService.Get( roomTypeId, ct );

            RouteValueDictionary routeValues = new()
            {
                { "id", roomTypeId }
            };

            return CreatedAtAction( nameof( RoomTypeController.GetById ), routeValues, createdRoomType );
        }
    }
}