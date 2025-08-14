using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.PropertiesApi
{
    /// <summary>Операции над услугами.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/amenities" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "Amenities" )]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenityController( IAmenityService amenityService )
        {
            _amenityService = amenityService;
        }

        /// <summary>Получить услугу по идентификатору.</summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Amenities_GetById", Summary = "Получить услугу по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( AmenityReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            AmenityReadDto dto = await _amenityService.Get( id, ct );

            return Ok( dto );
        }

        /// <summary>Получить список услуг.</summary>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet]
        [SwaggerOperation( OperationId = "Amenities_GetList", Summary = "Список услуг" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<AmenityReadDto> ) )]
        public async Task<IActionResult> GetList( CancellationToken ct )
        {
            IReadOnlyList<AmenityReadDto> getList = await _amenityService.GetList( ct );

            return Ok( getList );
        }

        /// <summary>Создать услугу.</summary>
        /// <param name="dto">Данные для создания.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPost]
        [SwaggerOperation( OperationId = "Amenities_Create", Summary = "Создать услугу" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( AmenityReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] AmenityCreateDto dto, CancellationToken ct )
        {
            int id = await _amenityService.Create( dto, ct );
            AmenityReadDto createdAmenity = await _amenityService.Get( id, ct );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, createdAmenity );
        }

        /// <summary>Обновить услугу.</summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Amenities_Update", Summary = "Обновить услугу" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] AmenityUpdateDto dto, CancellationToken ct )
        {
            await _amenityService.Update( id, dto, ct );

            return NoContent();
        }

        /// <summary>Удалить услугу.</summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Amenities_Remove", Summary = "Удалить услугу" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id, CancellationToken ct )
        {
            await _amenityService.Remove( id, ct );

            return NoContent();
        }
    }
}