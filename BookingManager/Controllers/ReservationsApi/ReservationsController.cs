using Application.DTOs;
using Application.DTOs.Reservations;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.ReservationsApi
{
    /// <summary>
    /// Операции над бронями.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/reservations" )]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController( IReservationService reservationService )
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Получить бронирование по идентификатору.
        /// </summary>
        /// <returns>Объект бронирования или код 404, если не найдено.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_GetById", Summary = "Получить бронирование по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            ReservationReadDto dto = await _reservationService.GetById( id );

            return Ok( dto );
        }

        /// <summary>
        /// Обновить бронирование.
        /// </summary>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_Update", Summary = "Обновить бронирование" )]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ReservationUpdateDto dto )
        {
            await _reservationService.Update( id, dto );

            return NoContent();
        }

        /// <summary>
        /// Удалить бронирование.
        /// </summary>
        /// <returns>Код 204 без содержимого при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Reservations_Delete", Summary = "Удалить бронирование" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _reservationService.Remove( id );

            return NoContent();
        }

        /// <summary>
        /// Получить список бронирований по критериям.
        /// </summary>
        /// <returns>Страница результатов с бронированиями.</returns>
        [HttpPost( "search" )]
        [SwaggerOperation( OperationId = "Reservations_GetList", Summary = "Список бронирований (поиск/фильтр)" )]
        public async Task<IActionResult> GetList(
            [FromBody] ReservationSearchQueryDto query )
        {
            PagedResultDto<ReservationReadDto> result = await _reservationService.GetByPage( query );

            return Ok( result );
        }

        /// <summary>
        /// Создать бронирование.
        /// </summary>
        /// <returns>Созданное бронирование с кодом 201 и заголовком Location.</returns>
        [HttpPost]
        [SwaggerOperation( OperationId = "Reservations_Create", Summary = "Создать бронирование" )]
        public async Task<IActionResult> Create(
            [FromBody] ReservationCreateDto dto )
        {
            int id = await _reservationService.Create( dto );
            ReservationReadDto created = await _reservationService.GetById( id );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, created );
        }
    }
}