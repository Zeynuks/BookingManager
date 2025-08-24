using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyReadDto> GetById( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<PropertyReadDto>> GetList( CancellationToken cancellationToken );
        Task<int> Create( PropertyCreateDto dto, CancellationToken cancellationToken );
        Task Update( int id, PropertyUpdateDto dto, CancellationToken cancellationToken );
        Task Remove( int id, CancellationToken cancellationToken );
    }
}