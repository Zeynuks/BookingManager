namespace Infrastructure.Foundation
{
    public interface IUnitOfWork
    {
        Task CommitAsync( CancellationToken ct );
    }
}