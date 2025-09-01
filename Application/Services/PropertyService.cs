using Application.DTOs.Properties;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Foundation;

namespace Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService( IPropertyRepository propertyRepository, IUnitOfWork unitOfWork )
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PropertyReadDto> GetById( int id )
        {
            Property? property = await _propertyRepository.TryGet( id );
            if ( property is null )
            {
                throw new DomainNotFoundException( $"Property with ID {id} could not be found." );
            }

            return new PropertyReadDto(
                property.Id,
                property.Name,
                property.Country,
                property.City,
                property.Address,
                property.Latitude,
                property.Longitude,
                property.Currency.ToString() );
        }

        public async Task<IReadOnlyList<PropertyReadDto>> GetList()
        {
            IReadOnlyList<Property> properties = await _propertyRepository.GetReadOnlyList();

            return properties
                .Select( property => new PropertyReadDto(
                    property.Id,
                    property.Name,
                    property.Country,
                    property.City,
                    property.Address,
                    property.Latitude,
                    property.Longitude,
                    property.Currency.ToString() ) )
                .ToList();
        }

        public async Task<int> Create( PropertyCreateDto dto )
        {
            if ( !Enum.TryParse( dto.Currency, true, out Currency currency ) )
            {
                throw new BusinessRuleViolationException( $"Invalid currency value: {dto.Currency}" );
            }

            Property property = new(
                dto.Name,
                dto.Country,
                dto.City,
                dto.Address,
                dto.Latitude,
                dto.Longitude,
                currency );

            _propertyRepository.Add( property );
            await _unitOfWork.CommitAsync();

            return property.Id;
        }

        public async Task Update( int id, PropertyUpdateDto dto )
        {
            Property? property = await _propertyRepository.TryGet( id );
            if ( property is null )
            {
                throw new DomainNotFoundException( $"Property with ID {id} could not be found." );
            }

            if ( !Enum.TryParse( dto.Currency, true, out Currency currency ) )
            {
                throw new BusinessRuleViolationException( $"Invalid currency value: {dto.Currency}" );
            }

            property.Update(
                dto.Name,
                dto.Country,
                dto.City,
                dto.Address,
                dto.Latitude,
                dto.Longitude,
                currency );

            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            Property? property = await _propertyRepository.TryGet( id );
            if ( property is null )
            {
                throw new DomainNotFoundException( $"Property with ID {id} could not be found." );
            }

            _propertyRepository.Delete( property );
            await _unitOfWork.CommitAsync();
        }
    }
}