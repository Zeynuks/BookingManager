using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<Property?> Get( int id, CancellationToken ct );
        Task<List<Property>> List( CancellationToken ct );
        void Add( Property property );
        void Delete( Property property );
    }
}