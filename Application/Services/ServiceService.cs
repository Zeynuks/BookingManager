using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;

namespace Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceService( IServiceRepository serviceRepository, IUnitOfWork unitOfWork )
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceReadDto> Get( int id, CancellationToken ct )
        {
            Service? service = await _serviceRepository.Get( id, ct );
            if ( service is null )
            {
                throw new DomainNotFoundException( $"Service with ID {id} could not be found." );
            }

            return new ServiceReadDto( service.Id, service.Name );
        }

        public async Task<IReadOnlyList<ServiceReadDto>> GetList( CancellationToken ct )
        {
            List<Service> services = await _serviceRepository.GetList( ct );

            return services.Select( service => new ServiceReadDto( service.Id, service.Name ) ).ToList();
        }

        public async Task<IReadOnlyList<ServiceReadDto>> GetListByRoomType( int roomTypeId, CancellationToken ct )
        {
            List<Service> services = await _serviceRepository.GetListByRoomType( roomTypeId, ct );

            return services.Select( service => new ServiceReadDto( service.Id, service.Name ) ).ToList();
        }

        public async Task<int> Create( ServiceCreateDto dto, CancellationToken ct )
        {
            Service service = new( dto.Name );

            _serviceRepository.Add( service );
            await _unitOfWork.CommitAsync( ct );

            return service.Id;
        }

        public async Task Update( int id, ServiceUpdateDto dto, CancellationToken ct )
        {
            Service? service = await _serviceRepository.Get( id, ct );
            if ( service is null )
            {
                throw new DomainNotFoundException( $"Service with ID {id} could not be found." );
            }

            service.Update( dto.Name );
            await _unitOfWork.CommitAsync( ct );
        }

        public async Task Remove( int id, CancellationToken ct )
        {
            Service? service = await _serviceRepository.Get( id, ct );
            if ( service is null )
            {
                throw new DomainNotFoundException( $"Service with ID {id} could not be found." );
            }

            _serviceRepository.Delete( service );
            await _unitOfWork.CommitAsync( ct );
        }
    }
}