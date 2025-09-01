using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;

namespace Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoomTypeService(
            IRoomTypeRepository roomTypeRepository,
            IPropertyRepository propertyRepository,
            IAmenityRepository amenityRepository,
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork )
        {
            _roomTypeRepository = roomTypeRepository;
            _propertyRepository = propertyRepository;
            _amenityRepository = amenityRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoomTypeReadDto> GetById( int id )
        {
            RoomType? roomType = await _roomTypeRepository.TryGet( id );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"RoomType with ID {id} could not be found." );
            }

            return new RoomTypeReadDto(
                roomType.Id,
                roomType.PropertyId,
                roomType.Name,
                roomType.DailyPrice,
                roomType.MaxPlaces,
                roomType.IsSharedOccupancy,
                roomType.Services.Select( s => new ServiceReadDto( s.Id, s.Name ) ).ToList(),
                roomType.Amenities.Select( a => new AmenityReadDto( a.Id, a.Name ) ).ToList() );
        }

        public async Task<IReadOnlyList<RoomTypeReadDto>> GetListByProperty( int propertyId )
        {
            Property? property = await _propertyRepository.TryGet( propertyId );
            if ( property is null )
            {
                throw new DomainNotFoundException( $"Property with ID {propertyId} could not be found." );
            }

            IReadOnlyList<RoomType> roomTypes =
                await _roomTypeRepository.GetListByProperty( property.Id );

            return roomTypes
                .Select( roomType => new RoomTypeReadDto(
                    roomType.Id,
                    roomType.PropertyId,
                    roomType.Name,
                    roomType.DailyPrice,
                    roomType.MaxPlaces,
                    roomType.IsSharedOccupancy,
                    roomType.Services.Select( s => new ServiceReadDto( s.Id, s.Name ) ).ToList(),
                    roomType.Amenities.Select( a => new AmenityReadDto( a.Id, a.Name ) ).ToList() ) )
                .ToList();
        }

        public async Task<int> Create( int propertyId, RoomTypeCreateDto dto )
        {
            Property? property = await _propertyRepository.TryGet( propertyId );
            if ( property is null )
            {
                throw new DomainNotFoundException( $"Property with ID {propertyId} could not be found." );
            }

            RoomType roomType = new(
                dto.Name,
                dto.DailyPrice,
                dto.MaxPlaces,
                dto.IsSharedOccupancy );

            if ( dto.ServiceIds is { Count: > 0 } )
            {
                IReadOnlyList<Service> services =
                    await _serviceRepository.GetReadOnlyListByIds( dto.ServiceIds );
                EnsureAllFound( dto.ServiceIds, services.Select( x => x.Id ).ToList(), "Service" );
                foreach ( Service s in services )
                {
                    roomType.AddService( s );
                }
            }

            if ( dto.AmenityIds is { Count: > 0 } )
            {
                IReadOnlyList<Amenity> amenities =
                    await _amenityRepository.GetReadOnlyListByIds( dto.AmenityIds );
                EnsureAllFound( dto.AmenityIds, amenities.Select( x => x.Id ).ToList(), "Amenity" );
                foreach ( Amenity a in amenities )
                {
                    roomType.AddAmenity( a );
                }
            }

            _roomTypeRepository.Add( roomType );
            await _unitOfWork.CommitAsync();

            return roomType.Id;
        }

        public async Task Update( int id, RoomTypeUpdateDto dto )
        {
            RoomType? roomType = await _roomTypeRepository.TryGet( id );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"RoomType with ID {id} could not be found." );
            }

            roomType.Update(
                dto.Name,
                dto.DailyPrice,
                dto.MaxPersonCount,
                dto.IsSharedOccupancy );

            if ( dto.ServiceIds is not null )
            {
                await SyncManyToMany(
                    roomType.Services,
                    dto.ServiceIds,
                    s => s.Id,
                    ids => _serviceRepository.GetReadOnlyListByIds( ids ),
                    ( service ) => roomType.AddService( service ),
                    ( service ) => roomType.RemoveService( service ) );
            }

            if ( dto.AmenityIds is not null )
            {
                await SyncManyToMany(
                    roomType.Amenities,
                    dto.AmenityIds,
                    a => a.Id,
                    ids => _amenityRepository.GetReadOnlyListByIds( ids ),
                    ( amenity ) => roomType.AddAmenity( amenity ),
                    ( amenity ) => roomType.RemoveAmenity( amenity ) );
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            RoomType? roomType = await _roomTypeRepository.TryGet( id );
            if ( roomType is null )
            {
                throw new DomainNotFoundException( $"RoomType with ID {id} could not be found." );
            }

            _roomTypeRepository.Delete( roomType );
            await _unitOfWork.CommitAsync();
        }

        private static void EnsureAllFound(
            IReadOnlyCollection<int> requestedIds,
            IReadOnlyCollection<int> foundIds,
            string name )
        {
            HashSet<int> missing = new( requestedIds );
            foreach ( int f in foundIds )
            {
                missing.Remove( f );
            }

            if ( missing.Count > 0 )
            {
                throw new DomainValidationException( $"{name} IDs not found: {string.Join( ",", missing )}" );
            }
        }

        private static async Task SyncManyToMany<TEntity>(
            IReadOnlyCollection<TEntity> currentEntities,
            IReadOnlyCollection<int> desiredIds,
            Func<TEntity, int> keySelector,
            Func<IReadOnlyCollection<int>, Task<IReadOnlyList<TEntity>>> loader,
            Action<TEntity> add,
            Action<TEntity> remove ) where TEntity : class
        {
            if ( desiredIds.Count == 0 )
            {
                RemoveAll( currentEntities, remove );
                return;
            }

            Dictionary<int, TEntity> currentById = BuildCurrentDictionary( currentEntities, keySelector );

            HashSet<int> desired = new( desiredIds );

            if ( IsSameSet( desired, currentById ) )
            {
                return;
            }

            RemoveExtra( currentById, desired, remove );
            await AddMissing( desired, currentById, loader, add, keySelector );
        }

        private static void RemoveAll<TEntity>(
            IReadOnlyCollection<TEntity> currentEntities,
            Action<TEntity> remove )
        {
            foreach ( TEntity e in currentEntities )
            {
                remove( e );
            }
        }

        private static Dictionary<int, TEntity> BuildCurrentDictionary<TEntity>(
            IReadOnlyCollection<TEntity> currentEntities,
            Func<TEntity, int> keySelector )
        {
            Dictionary<int, TEntity> dict = new( currentEntities.Count );
            foreach ( TEntity e in currentEntities )
            {
                dict[ keySelector( e ) ] = e;
            }

            return dict;
        }

        private static bool IsSameSet<TEntity>(
            HashSet<int> desired,
            Dictionary<int, TEntity> currentById )
        {
            return desired.Count == currentById.Count && desired.All( currentById.ContainsKey );
        }

        private static void RemoveExtra<TEntity>(
            Dictionary<int, TEntity> currentById,
            HashSet<int> desired,
            Action<TEntity> remove )
        {
            foreach ( KeyValuePair<int, TEntity> kv in currentById.Where( kv => !desired.Contains( kv.Key ) ) )
            {
                remove( kv.Value );
            }
        }

        private static async Task AddMissing<TEntity>(
            HashSet<int> desired,
            Dictionary<int, TEntity> currentById,
            Func<IReadOnlyCollection<int>, Task<IReadOnlyList<TEntity>>> loader,
            Action<TEntity> add,
            Func<TEntity, int> keySelector )
        {
            IReadOnlyList<int> toAddIds = desired.Where( id => !currentById.ContainsKey( id ) ).ToList();
            if ( toAddIds.Count > 0 )
            {
                IReadOnlyList<TEntity> toAdd = await loader( toAddIds );
                EnsureAllFound( toAddIds, toAdd.Select( keySelector ).ToList(), typeof( TEntity ).Name );
                foreach ( TEntity e in toAdd )
                {
                    add( e );
                }
            }
        }
    }
}