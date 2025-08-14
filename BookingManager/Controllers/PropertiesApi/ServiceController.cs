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
        /// <param name="ct">Токен отмены.</param>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Services_GetById", Summary = "Получить сервис по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( ServiceReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            ServiceReadDto dto = await _serviceService.Get( id, ct );

            return Ok( dto );
        }

        /// <summary>Получить список сервисов.</summary>
        /// <param name="ct">Токен отмены.</param>
        [HttpGet]
        [SwaggerOperation( OperationId = "Services_GetList", Summary = "Список сервисов" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<ServiceReadDto> ) )]
        public async Task<IActionResult> GetList( CancellationToken ct )
        {
            IReadOnlyList<ServiceReadDto> getList = await _serviceService.GetList( ct );

            return Ok( getList );
        }

        /// <summary>Создать сервис.</summary>
        /// <param name="dto">Данные для создания.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPost]
        [SwaggerOperation( OperationId = "Services_Create", Summary = "Создать сервис" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( ServiceReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] ServiceCreateDto dto, CancellationToken ct )
        {
            int id = await _serviceService.Create( dto, ct );
            ServiceReadDto createdService = await _serviceService.Get( id, ct );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, createdService );
        }

        /// <summary>Обновить сервис.</summary>
        /// <param name="id">Идентификатор сервиса.</param>
        /// <param name="dto">Новые данные.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Services_Update", Summary = "Обновить сервис" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] ServiceUpdateDto dto, CancellationToken ct )
        {
            await _serviceService.Update( id, dto, ct );

            return NoContent();
        }

        /// <summary>Удалить сервис.</summary>
        /// <param name="id">Идентификатор сервиса.</param>
        /// <param name="ct">Токен отмены.</param>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Services_Remove", Summary = "Удалить сервис" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id, CancellationToken ct )
        {
            await _serviceService.Remove( id, ct );

            return NoContent();
        }
    }
}