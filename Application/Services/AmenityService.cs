using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;

namespace Application.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AmenityService( IAmenityRepository amenityRepository, IUnitOfWork unitOfWork )
        {
            _amenityRepository = amenityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AmenityReadDto> GetById( int id, CancellationToken cancellationToken )
        {
            Amenity? amenity = await _amenityRepository.TryGet( id, cancellationToken );
            if ( amenity is null )
            {
                throw new DomainNotFoundException( $"Amenity with ID {id} could not be found." );
            }

            return new AmenityReadDto( amenity.Id, amenity.Name );
        }

        public async Task<IReadOnlyList<AmenityReadDto>> GetList( CancellationToken cancellationToken )
        {
            IReadOnlyList<Amenity> amenities = await _amenityRepository.GetList( cancellationToken );

            return amenities.Select( amenity => new AmenityReadDto( amenity.Id, amenity.Name ) ).ToList();
        }

        public async Task<int> Create( AmenityCreateDto dto, CancellationToken cancellationToken )
        {
            Amenity amenity = new( dto.Name );

            _amenityRepository.Add( amenity );
            await _unitOfWork.CommitAsync( cancellationToken );

            return amenity.Id;
        }

        public async Task Update( int id, AmenityUpdateDto dto, CancellationToken cancellationToken )
        {
            Amenity? amenity = await _amenityRepository.TryGet( id, cancellationToken );
            if ( amenity is null )
            {
                throw new DomainNotFoundException( $"Amenity with ID {id} could not be found." );
            }

            amenity.Update( dto.Name );
            await _unitOfWork.CommitAsync( cancellationToken );
        }

        public async Task Remove( int id, CancellationToken cancellationToken )
        {
            Amenity? amenity = await _amenityRepository.TryGet( id, cancellationToken );
            if ( amenity is null )
            {
                throw new DomainNotFoundException( $"Amenity with ID {id} could not be found." );
            }

            _amenityRepository.Delete( amenity );
            await _unitOfWork.CommitAsync( cancellationToken );
        }
    }
}