using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyReadDto> Get( int id, CancellationToken ct );
        Task<IReadOnlyList<PropertyReadDto>> GetList( CancellationToken ct );
        Task<int> Create( PropertyCreateDto dto, CancellationToken ct );
        Task Update( int id, PropertyUpdateDto dto, CancellationToken ct );
        Task Remove( int id, CancellationToken ct );
    }
}