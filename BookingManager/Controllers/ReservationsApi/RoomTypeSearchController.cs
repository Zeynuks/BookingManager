using Application.DTOs;
using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingManager.Controllers.ReservationsApi
{
    /// <summary>Поиск вариантов размещения.</summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "reservations" )]
    [Route( "api/search" )]
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [Tags( "Search" )]
    public class RoomTypeSearchController : ControllerBase
    {
        private readonly IRoomTypeSearchService _roomTypeSearchService;

        public RoomTypeSearchController( IRoomTypeSearchService roomTypeSearchService )
        {
            _roomTypeSearchService = roomTypeSearchService;
        }

        /// <summary>Поиск типов комнат.</summary>
        /// <remarks>Фильтры: город, даты, гости, цена, удобства/сервисы.</remarks>
        [HttpGet]
        [SwaggerOperation( OperationId = "Search_GetRoomTypes", Summary = "Поиск типов комнат" )]
        [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( PagedResultDto<RoomTypeReadDto> ) )]
        public async Task<IActionResult> Get( [FromQuery] RoomTypeSearchQueryDto query, CancellationToken ct )
        {
            PagedResultDto<RoomTypeReadDto> result = await _roomTypeSearchService.GetList( query, ct );

            return Ok( result );
        }
    }
}