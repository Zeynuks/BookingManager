using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.PropertiesApi
{
    /// <summary>
    /// Операции над услугами.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/amenities" )]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenityController( IAmenityService amenityService )
        {
            _amenityService = amenityService;
        }

        /// <summary>
        /// Получить услугу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <returns>Объект услуги или код 404, если не найдено.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Amenities_GetById", Summary = "Получить услугу по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            AmenityReadDto dto = await _amenityService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список услуг.
        /// </summary>
        /// <returns>Список услуг.</returns>
        [HttpGet]
        [SwaggerOperation( OperationId = "Amenities_GetList", Summary = "Список услуг" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<AmenityReadDto> getList = await _amenityService.GetList();
            return Ok( getList );
        }

        /// <summary>
        /// Создать услугу.
        /// </summary>
        /// <param name="dto">Данные для создания.</param>
        /// <returns>Созданная услуга с кодом 201 и заголовком Location.</returns>
        [HttpPost]
        [SwaggerOperation( OperationId = "Amenities_Create", Summary = "Создать услугу" )]
        public async Task<IActionResult> Create( [FromBody] AmenityCreateDto dto )
        {
            int id = await _amenityService.Create( dto );
            AmenityReadDto createdAmenity = await _amenityService.GetById( id );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, createdAmenity );
        }

        /// <summary>
        /// Обновить услугу.
        /// </summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <param name="dto">Новые данные.</param>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Amenities_Update", Summary = "Обновить услугу" )]
        public async Task<IActionResult> Update( int id, [FromBody] AmenityUpdateDto dto )
        {
            await _amenityService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Удалить услугу.
        /// </summary>
        /// <param name="id">Идентификатор услуги.</param>
        /// <returns>Код 204 без содержимого при успешном удалении; 404 если не найдена.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Amenities_Remove", Summary = "Удалить услугу" )]
        public async Task<IActionResult> Remove( int id )
        {
            await _amenityService.Remove( id );
            return NoContent();
        }
    }
}