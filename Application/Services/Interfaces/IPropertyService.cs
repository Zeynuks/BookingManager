using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyReadDto> GetById( int id );
        Task<IReadOnlyList<PropertyReadDto>> GetList();
        Task<int> Create( PropertyCreateDto dto );
        Task Update( int id, PropertyUpdateDto dto );
        Task Remove( int id );
    }
}