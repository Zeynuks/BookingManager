using Application.DTOs;
using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.ReservationsApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/search" )]
    [Produces( "application/json" )]
    public class RoomTypeSearchController : ControllerBase
    {
        private readonly IRoomTypeSearchService _roomTypeSearchService;

        public RoomTypeSearchController( IRoomTypeSearchService roomTypeSearchService )
        {
            _roomTypeSearchService = roomTypeSearchService;
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<IActionResult> Get( [FromQuery] RoomTypeSearchQueryDto query, CancellationToken ct )
        {
            PagedResultDto<RoomTypeReadDto> result = await _roomTypeSearchService.List( query, ct );

            return Ok( result );
        }
    }
}