using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<Property?> TryGet( int id );
        Task<IReadOnlyList<Property>> GetReadOnlyList();
        void Add( Property property );
        void Delete( Property property );
    }
}