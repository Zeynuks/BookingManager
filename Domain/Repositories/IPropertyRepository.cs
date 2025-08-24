using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<Property?> TryGet( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<Property>> GetList( CancellationToken cancellationToken );
        void Add( Property property );
        void Delete( Property property );
    }
}