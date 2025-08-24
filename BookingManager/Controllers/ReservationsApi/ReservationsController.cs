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
        private readonly IReservationService _reservationService;

        public ReservationsController( IReservationService reservationService )
        {
            _reservationService = reservationService;
        }

        /// <summary>Получить бронирование по идентификатору.</summary>
        /// <returns>Объект бронирования или код 404, если не найдено.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_GetById", Summary = "Получить бронирование по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( ReservationReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken cancellationToken )
        {
            ReservationReadDto dto = await _reservationService.GetById( id, cancellationToken );

            return Ok( dto );
        }

        /// <summary>Обновить бронирование.</summary>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_Update", Summary = "Обновить бронирование" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ReservationUpdateDto dto,
            CancellationToken cancellationToken )
        {
            await _reservationService.Update( id, dto, cancellationToken );

            return NoContent();
        }

        /// <summary>Удалить бронирование.</summary>
        /// <returns>Код 204 без содержимого при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_Delete", Summary = "Удалить бронирование" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        public async Task<IActionResult> Delete( int id, CancellationToken cancellationToken )
        {
            await _reservationService.Remove( id, cancellationToken );

            return NoContent();
        }

        /// <summary>Получить список бронирований по критериям.</summary>
        /// <returns>Страница результатов с бронированиями.</returns>
        [HttpPost( "search" )]
        [SwaggerOperation( OperationId = "Reservations_GetList", Summary = "Список бронирований (поиск/фильтр)" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( PagedResultDto<ReservationReadDto> ) )]
        public async Task<IActionResult> GetList(
            [FromBody] ReservationSearchQueryDto query,
            CancellationToken cancellationToken )
        {
            PagedResultDto<ReservationReadDto> result = await _reservationService.GetByPage( query, cancellationToken );

            return Ok( result );
        }


        /// <summary>Создать бронирование.</summary>
        /// <returns>Созданное бронирование с кодом 201 и заголовком Location.</returns>
        [HttpPost]
        [SwaggerOperation( OperationId = "Reservations_Create", Summary = "Создать бронирование" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( ReservationReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create(
            [FromBody] ReservationCreateDto dto,
            CancellationToken cancellationToken )
        {
            int id = await _reservationService.Create( dto, cancellationToken );
            ReservationReadDto created = await _reservationService.GetById( id, cancellationToken );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, created );
        }
    }
}