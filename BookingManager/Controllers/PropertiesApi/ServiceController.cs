using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.PropertiesApi
{
    /// <summary>Операции над сервисами.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/services" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "Services" )]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController( IServiceService serviceService )
        {
            _serviceService = serviceService;
        }

        /// <summary>Получить сервис по идентификатору.</summary>
        /// <param name="id">Идентификатор сервиса.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Объект сервиса или код 404, если не найден.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Services_GetById", Summary = "Получить сервис по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( ServiceReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken cancellationToken )
        {
            ServiceReadDto dto = await _serviceService.GetById( id, cancellationToken );

            return Ok( dto );
        }

        /// <summary>Получить список сервисов.</summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список сервисов.</returns>
        [HttpGet]
        [SwaggerOperation( OperationId = "Services_GetList", Summary = "Список сервисов" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<ServiceReadDto> ) )]
        public async Task<IActionResult> GetList( CancellationToken cancellationToken )
        {
            IReadOnlyList<ServiceReadDto> getList = await _serviceService.GetList( cancellationToken );

            return Ok( getList );
        }

        /// <summary>Создать сервис.</summary>
        /// <param name="dto">Данные для создания.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданный сервис с кодом 201 и заголовком Location.</returns>
        [HttpPost]
        [SwaggerOperation( OperationId = "Services_Create", Summary = "Создать сервис" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( ServiceReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] ServiceCreateDto dto, CancellationToken cancellationToken )
        {
            int id = await _serviceService.Create( dto, cancellationToken );
            ServiceReadDto createdService = await _serviceService.GetById( id, cancellationToken );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, createdService );
        }

        /// <summary>Обновить сервис.</summary>
        /// <param name="id">Идентификатор сервиса.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Services_Update", Summary = "Обновить сервис" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] ServiceUpdateDto dto,
            CancellationToken cancellationToken )
        {
            await _serviceService.Update( id, dto, cancellationToken );

            return NoContent();
        }

        /// <summary>Удалить сервис.</summary>
        /// <param name="id">Идентификатор сервиса.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код 204 без содержимого при успешном удалении; 404 если не найден.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Services_Remove", Summary = "Удалить сервис" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id, CancellationToken cancellationToken )
        {
            await _serviceService.Remove( id, cancellationToken );

            return NoContent();
        }
    }
}