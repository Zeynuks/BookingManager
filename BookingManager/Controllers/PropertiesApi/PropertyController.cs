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
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданный отель с кодом 201 и заголовком Location.</returns>
        [HttpPost]
        [SwaggerOperation( OperationId = "Properties_Create", Summary = "Создать отель" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( PropertyReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] PropertyCreateDto dto, CancellationToken cancellationToken )
        {
            int id = await _propertyService.Create( dto, cancellationToken );
            PropertyReadDto createdProperty = await _propertyService.GetById( id, cancellationToken );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, createdProperty );
        }

        /// <summary>Получить список отелей.</summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список отелей.</returns>
        [HttpGet]
        [SwaggerOperation( OperationId = "Properties_GetList", Summary = "Список отелей" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<PropertyReadDto> ) )]
        public async Task<IActionResult> GetList( CancellationToken cancellationToken )
        {
            IReadOnlyList<PropertyReadDto> getList = await _propertyService.GetList( cancellationToken );

            return Ok( getList );
        }

        /// <summary>Получить отель по идентификатору.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Объект отеля или код 404, если не найден.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Properties_GetById", Summary = "Получить отель по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( PropertyReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken cancellationToken )
        {
            PropertyReadDto dto = await _propertyService.GetById( id, cancellationToken );

            return Ok( dto );
        }

        /// <summary>Обновить отель.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Properties_Update", Summary = "Обновить отель" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] PropertyUpdateDto dto,
            CancellationToken cancellationToken )
        {
            await _propertyService.Update( id, dto, cancellationToken );

            return NoContent();
        }

        /// <summary>Удалить отель.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном удалении; 404 если не найден.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Properties_Delete", Summary = "Удалить отель" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken cancellationToken )
        {
            await _propertyService.Remove( id, cancellationToken );

            return NoContent();
        }

        /// <summary>Получить типы комнат для отеля.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список типов комнат для указанного отеля.</returns>
        [HttpGet( "{id:int}/room-types" )]
        [SwaggerOperation( OperationId = "Properties_GetRoomTypes", Summary = "Типы комнат отеля" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<RoomTypeDto> ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetRoomTypes( int id, CancellationToken cancellationToken )
        {
            IReadOnlyList<RoomTypeDto> getList = await _roomTypeService.GetListByProperty( id, cancellationToken );

            return Ok( getList );
        }

        /// <summary>Создать тип комнаты в отеле.</summary>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="dto">Данные для создания типа комнаты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданный тип комнаты с кодом 201 и заголовком Location.</returns>
        [HttpPost( "{id:int}/room-types" )]
        [SwaggerOperation( OperationId = "Properties_CreateRoomType", Summary = "Создать тип комнаты в отеле" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( RoomTypeDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> CreateRoomType( int id, [FromBody] RoomTypeCreateDto dto,
            CancellationToken cancellationToken )
        {
            int roomTypeId = await _roomTypeService.Create( id, dto, cancellationToken );
            RoomTypeDto createdRoomType = await _roomTypeService.GetById( roomTypeId, cancellationToken );

            RouteValueDictionary routeValues = new()
            {
                { "id", roomTypeId }
            };

            return CreatedAtAction( nameof( RoomTypeController.GetById ), routeValues, createdRoomType );
        }
    }
}