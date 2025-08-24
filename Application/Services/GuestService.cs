using Application.DTOs.Reservations;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;

namespace Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GuestService( IGuestRepository guestRepository, IUnitOfWork unitOfWork )
        {
            _guestRepository = guestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GuestReadDto> GetById( int id, CancellationToken cancellationToken )
        {
            Guest? guest = await _guestRepository.TryGet( id, cancellationToken );
            if ( guest is null )
            {
                throw new DomainNotFoundException( $"Guest with ID {id} could not be found." );
            }

            return new GuestReadDto( guest.Id, guest.Name, guest.PhoneNumber );
        }

        public async Task<IReadOnlyList<GuestReadDto>> GetList( CancellationToken cancellationToken )
        {
            IReadOnlyList<Guest> guests = await _guestRepository.GetList( cancellationToken );

            return guests.Select( guest => new GuestReadDto( guest.Id, guest.Name, guest.PhoneNumber ) ).ToList();
        }

        public async Task<int> Create( GuestCreateDto dto, CancellationToken cancellationToken )
        {
            Guest guest = new( dto.Name, dto.PhoneNumber );

            _guestRepository.Add( guest );
            await _unitOfWork.CommitAsync( cancellationToken );

            return guest.Id;
        }

        public async Task Update( int id, GuestUpdateDto dto, CancellationToken cancellationToken )
        {
            Guest? guest = await _guestRepository.TryGet( id, cancellationToken );
            if ( guest is null )
            {
                throw new DomainNotFoundException( $"Guest with ID {id} could not be found." );
            }

            guest.Update( dto.Name, dto.PhoneNumber );
            await _unitOfWork.CommitAsync( cancellationToken );
        }

        public async Task Remove( int id, CancellationToken cancellationToken )
        {
            Guest? guest = await _guestRepository.TryGet( id, cancellationToken );
            if ( guest is null )
            {
                throw new DomainNotFoundException( $"Guest with ID {id} could not be found." );
            }

            _guestRepository.Delete( guest );
            await _unitOfWork.CommitAsync( cancellationToken );
        }
    }
}