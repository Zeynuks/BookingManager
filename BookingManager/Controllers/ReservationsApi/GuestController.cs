using Application.DTOs.Reservations;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.ReservationsApi
{
    /// <summary>
    /// Операции над гостями.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/guests" )]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController( IGuestService guestService )
        {
            _guestService = guestService;
        }

        /// <summary>
        /// Получить гостя по идентификатору.
        /// </summary>
        /// <returns>Объект гостя или код 404, если не найдено.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Guests_GetById", Summary = "Получить гостя по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( GuestReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id )
        {
            GuestReadDto dto = await _guestService.GetById( id );

            return Ok( dto );
        }

        /// <summary>
        /// Получить список гостей.
        /// </summary>
        /// <returns>Список гостей.</returns>
        [HttpGet]
        [SwaggerOperation( OperationId = "Guests_GetList", Summary = "Список гостей" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<GuestReadDto> ) )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<GuestReadDto> getList = await _guestService.GetList();

            return Ok( getList );
        }

        /// <summary>
        /// Создать гостя.
        /// </summary>
        /// <returns>Созданный гость с кодом 201 и заголовком Location.</returns>
        [HttpPost]
        [SwaggerOperation( OperationId = "Guests_Create", Summary = "Создать гостя" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( GuestReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] GuestCreateDto dto )
        {
            int id = await _guestService.Create( dto );
            GuestReadDto created = await _guestService.GetById( id );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, created );
        }

        /// <summary>
        /// Обновить данные гостя.
        /// </summary>
        /// <returns>Код 204 без содержимого при успешном обновлении; 400/404 при ошибке.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Guests_Update", Summary = "Обновить гостя" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] GuestUpdateDto dto )
        {
            await _guestService.Update( id, dto );

            return NoContent();
        }

        /// <summary>
        /// Удалить гостя.
        /// </summary>
        /// <returns>Код 204 без содержимого при успешном удалении; 404 если не найден.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Guests_Remove", Summary = "Удалить гостя" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id )
        {
            await _guestService.Remove( id );

            return NoContent();
        }
    }
}