using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.Controllers.PropertiesApi
{
    [ApiController]
    [ApiExplorerSettings( GroupName = "properties" )]
    [Route( "api/properties" )]
    [Produces( "application/json" )]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IRoomTypeService _roomTypeService;

        public PropertyController(
            IPropertyService propertyService,
            IRoomTypeService roomTypeService )
        {
            _propertyService = propertyService;
            _roomTypeService = roomTypeService;
        }

        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> Create( [FromBody] PropertyCreateDto dto, CancellationToken ct )
        {
            int id = await _propertyService.Create( dto, ct );

            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, null );
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<IActionResult> GetAll( CancellationToken ct )
        {
            IReadOnlyList<PropertyReadDto> list = await _propertyService.List( ct );

            return Ok( list );
        }

        [HttpGet( "{id:int}", Name = "GetPropertyById" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetById( int id, CancellationToken ct )
        {
            PropertyReadDto dto = await _propertyService.Get( id, ct );

            return Ok( dto );
        }

        [HttpPut( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Update( int id, [FromBody] PropertyUpdateDto dto, CancellationToken ct )
        {
            await _propertyService.Update( id, dto, ct );

            return NoContent();
        }

        [HttpDelete( "{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> Delete( int id, CancellationToken ct )
        {
            await _propertyService.Remove( id, ct );

            return NoContent();
        }

        // Мне показалось, что логично расположить их тут, но есть сомнения,
        // тк если их вынести в RoomTypeController то не будет зависимости от RoomTypeService,
        // но там придётся переопределять роуты
        // Или же стоит вынести в отдельный сервисный слой?
        [HttpGet( "{id:int}/room-types" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> GetRoomTypes( int id, CancellationToken ct )
        {
            IReadOnlyList<RoomTypeDto> items = await _roomTypeService.ListByProperty( id, ct );

            return Ok( items );
        }

        [HttpPost( "{id:int}/room-types" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> CreateRoomType( int id, [FromBody] RoomTypeCreateDto dto,
            CancellationToken ct )
        {
            int roomTypeId = await _roomTypeService.Create( id, dto, ct );

            return CreatedAtRoute( "GetRoomTypeById", new
            {
                roomTypeId
            }, null );
        }
    }
}