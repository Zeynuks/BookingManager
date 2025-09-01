using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomTypeReadDto> GetById( int id );
        Task<IReadOnlyList<RoomTypeReadDto>> GetListByProperty( int propertyId );
        Task<int> Create( int propertyId, RoomTypeCreateDto dto );
        Task Update( int id, RoomTypeUpdateDto dto );
        Task Remove( int id );
    }
}