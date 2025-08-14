using Application.DTOs.Reservations;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.ReservationsApi
{
    /// <summary>Операции над гостями.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/guests" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "Guests" )]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController( IGuestService guestService )
        {
            _guestService = guestService;
        }

        /// <summary>Получить гостя по идентификатору.</summary>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "Guests_GetById", Summary = "Получить гостя по Id" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( GuestReadDto ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            GuestReadDto dto = await _guestService.Get( id, ct );

            return Ok( dto );
        }

        /// <summary>Получить список гостей.</summary>
        [HttpGet]
        [SwaggerOperation( OperationId = "Guests_GetList", Summary = "Список гостей" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( IReadOnlyList<GuestReadDto> ) )]
        public async Task<IActionResult> GetList( CancellationToken ct )
        {
            IReadOnlyList<GuestReadDto> getList = await _guestService.GetList( ct );

            return Ok( getList );
        }

        /// <summary>Создать гостя.</summary>
        [HttpPost]
        [SwaggerOperation( OperationId = "Guests_Create", Summary = "Создать гостя" )]
        [ProducesResponseType( StatusCodes.Status201Created, Type = typeof( GuestReadDto ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] GuestCreateDto dto, CancellationToken ct )
        {
            int id = await _guestService.Create( dto, ct );
            GuestReadDto created = await _guestService.Get( id, ct );

            RouteValueDictionary routeValues = new()
            {
                { "id", id }
            };

            return CreatedAtAction( nameof( GetById ), routeValues, created );
        }

        /// <summary>Обновить данные гостя.</summary>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "Guests_Update", Summary = "Обновить гостя" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] GuestUpdateDto dto, CancellationToken ct )
        {
            await _guestService.Update( id, dto, ct );

            return NoContent();
        }

        /// <summary>Удалить гостя.</summary>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "Guests_Remove", Summary = "Удалить гостя" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id, CancellationToken ct )
        {
            await _guestService.Remove( id, ct );

            return NoContent();
        }
    }
}