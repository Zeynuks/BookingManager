using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.PropertiesApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/amenities" )]
    [Produces( "application/json" )]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenityController( IAmenityService amenityService )
        {
            _amenityService = amenityService;
        }

        [HttpGet( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            AmenityReadDto dto = await _amenityService.Get( id, ct );
            return Ok( dto );
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<IActionResult> List( CancellationToken ct )
        {
            IReadOnlyList<AmenityReadDto> dtos = await _amenityService.List( ct );
            return Ok( dtos );
        }

        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] AmenityCreateDto dto, CancellationToken ct )
        {
            int id = await _amenityService.Create( dto, ct );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, null );
        }

        [HttpPut( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] AmenityUpdateDto dto, CancellationToken ct )
        {
            await _amenityService.Update( id, dto, ct );
            return NoContent();
        }

        [HttpDelete( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id, CancellationToken ct )
        {
            await _amenityService.Remove( id, ct );
            return NoContent();
        }
    }
}