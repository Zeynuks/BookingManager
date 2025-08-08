namespace Domain.Repository
{
    public interface IRepository<T>
    {
        Task<T?> Get( int id );

        Task<List<T>> List();
        
        void Add( T actor );

        void Delete( T actor );
    }
}