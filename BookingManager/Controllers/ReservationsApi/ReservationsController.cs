using Application.DTOs;
using Application.DTOs.Reservations;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.ReservationsApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/reservations" )]
    [Produces( "application/json" )]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController( IReservationService service )
        {
            _service = service;
        }

        [HttpGet( "{id:int}", Name = "GetReservationById" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            ReservationReadDto dto = await _service.Get( id, ct );
            return Ok( dto );
        }

        [HttpPut( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] ReservationUpdateDto dto, CancellationToken ct )
        {
            await _service.Update( id, dto, ct );
            return NoContent();
        }

        [HttpDelete( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _service.Remove( id, ct );
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<IActionResult> List( [FromQuery] ReservationSearchQueryDto query, CancellationToken ct )
        {
            PagedResultDto<ReservationReadDto> result = await _service.List( query, ct );
            return Ok( result );
        }

        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] ReservationCreateDto dto, CancellationToken ct )
        {
            int id = await _service.Create( dto, ct );
            return CreatedAtRoute( "GetReservationById", new
            {
                id
            }, null );
        }
    }
}