using Application.DTOs;
using Application.DTOs.Reservations;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.ReservationsApi
{
    /// <summary>Операции над бронями.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/reservations" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "Reservations" )]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController( IReservationService service )
        {
            _service = service;
        }

        /// <summary>Получить бронирование по идентификатору.</summary>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_GetById", Summary = "Получить бронирование по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( ReservationReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            ReservationReadDto dto = await _service.Get( id, ct );

            return Ok( dto );
        }

        /// <summary>Обновить бронирование.</summary>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_Update", Summary = "Обновить бронирование" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] ReservationUpdateDto dto, CancellationToken ct )
        {
            await _service.Update( id, dto, ct );

            return NoContent();
        }

        /// <summary>Удалить бронирование.</summary>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_Delete", Summary = "Удалить бронирование" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _service.Remove( id, ct );

            return NoContent();
        }

        /// <summary>Получить список бронирований по критериям.</summary>
        [HttpGet]
        [SwaggerOperation( OperationId = "Reservations_GetList", Summary = "Список бронирований (поиск/фильтр)" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( PagedResultDto<ReservationReadDto> ) )]
        public async Task<IActionResult> GetList( [FromQuery] ReservationSearchQueryDto query, CancellationToken ct )
        {
            PagedResultDto<ReservationReadDto> result = await _service.GetList( query, ct );

            return Ok( result );
        }

        /// <summary>Создать бронирование.</summary>
        [HttpPost]
        [SwaggerOperation( OperationId = "Reservations_Create", Summary = "Создать бронирование" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( ReservationReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] ReservationCreateDto dto, CancellationToken ct )
        {
            int id = await _service.Create( dto, ct );
            ReservationReadDto created = await _service.Get( id, ct );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, created );
        }
    }
}