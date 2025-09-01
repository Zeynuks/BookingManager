using Application.DTOs.Properties;

namespace Application.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceReadDto> GetById( int id );
        Task<IReadOnlyList<ServiceReadDto>> GetList();
        Task<int> Create( ServiceCreateDto dto );
        Task Update( int id, ServiceUpdateDto dto );
        Task Remove( int id );
    }
}