using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IAmenityService
    {
        Task<AmenityReadDto> GetById( int id );
        Task<IReadOnlyList<AmenityReadDto>> GetList();
        Task<int> Create( AmenityCreateDto dto );
        Task Update( int id, AmenityUpdateDto dto );
        Task Remove( int id );
    }
}