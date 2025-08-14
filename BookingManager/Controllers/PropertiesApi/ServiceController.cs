using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.PropertiesApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/services" )]
    [Produces( "application/json" )]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController( IServiceService serviceService )
        {
            _serviceService = serviceService;
        }

        [HttpGet( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            ServiceReadDto dto = await _serviceService.Get( id, ct );
            return Ok( dto );
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<IActionResult> List( CancellationToken ct )
        {
            IReadOnlyList<ServiceReadDto> dtos = await _serviceService.List( ct );
            return Ok( dtos );
        }

        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] ServiceCreateDto dto, CancellationToken ct )
        {
            int id = await _serviceService.Create( dto, ct );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, null );
        }

        [HttpPut( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] ServiceUpdateDto dto, CancellationToken ct )
        {
            await _serviceService.Update( id, dto, ct );
            return NoContent();
        }

        [HttpDelete( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Remove( int id, CancellationToken ct )
        {
            await _serviceService.Remove( id, ct );
            return NoContent();
        }
    }
}