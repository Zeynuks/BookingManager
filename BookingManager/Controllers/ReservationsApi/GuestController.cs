using Application.DTOs.Reservations;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.ReservationsApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/guests" )]
    [Produces( "application/json" )]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController( IGuestService guestService )
        {
            _guestService = guestService;
        }

        [HttpGet( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            GuestReadDto dto = await _guestService.Get( id, ct );
            return Ok( dto );
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<IActionResult> List( CancellationToken ct )
        {
            IReadOnlyList<GuestReadDto> dtos = await _guestService.List( ct );
            return Ok( dtos );
        }

        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] GuestCreateDto dto, CancellationToken ct )
        {
            int id = await _guestService.Create( dto, ct );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, null );
        }

        [HttpPut( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] GuestUpdateDto dto, CancellationToken ct )
        {
            await _guestService.Update( id, dto, ct );
            return NoContent();
        }

        [HttpDelete( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id, CancellationToken ct )
        {
            await _guestService.Remove( id, ct );
            return NoContent();
        }
    }
}