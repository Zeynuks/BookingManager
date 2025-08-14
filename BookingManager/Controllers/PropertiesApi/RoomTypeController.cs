using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.PropertiesApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/room-types" )]
    [Produces( "application/json" )]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRoomService _roomService;
        private readonly IServiceService _serviceService;
        private readonly IAmenityService _amenityService;

        public RoomTypeController(
            IRoomTypeService roomTypeService,
            IRoomService roomService,
            IServiceService serviceService,
            IAmenityService amenityService )
        {
            _roomTypeService = roomTypeService;
            _roomService = roomService;
            _serviceService = serviceService;
            _amenityService = amenityService;
        }

        [HttpGet( "{id:int}", Name = "GetRoomTypeById" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            RoomTypeDto dto = await _roomTypeService.Get( id, ct );

            return Ok( dto );
        }

        [HttpPut( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] RoomTypeUpdateDto dto, CancellationToken ct )
        {
            await _roomTypeService.Update( id, dto, ct );

            return NoContent();
        }

        [HttpDelete( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _roomTypeService.Remove( id, ct );

            return NoContent();
        }
        
        [HttpGet( "{id:int}/rooms" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetRooms( int roomTypeId, CancellationToken ct )
        {
            IReadOnlyList<RoomReadDto> items = await _roomService.ListByRoomTypeId( roomTypeId, ct );

            return Ok( items );
        }

        [HttpPost( "{id:int}/rooms" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> CreateRoom( int roomTypeId, [FromBody] RoomCreateDto dto,
            CancellationToken ct )
        {
            int id = await _roomService.Create( roomTypeId, dto, ct );

            return CreatedAtRoute( "GetRoomById", new
            {
                id
            }, null );
        }

        [HttpGet( "{id:int}/services" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetServices( int roomTypeId, CancellationToken ct )
        {
            IReadOnlyList<ServiceReadDto> items = await _serviceService.ListByRoomType( roomTypeId, ct );

            return Ok( items );
        }

        [HttpGet( "{id:int}/amenities" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetAmenities( int roomTypeId, CancellationToken ct )
        {
            IReadOnlyList<AmenityReadDto> items = await _amenityService.ListByRoomType( roomTypeId, ct );

            return Ok( items );
        }
    }
}