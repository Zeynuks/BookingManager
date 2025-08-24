namespace Application.DTOs
{
    public class PagedResultDto<T>
    {
        public IReadOnlyCollection<T> Items { get; init; } = Array.Empty<T>();
        public int Total { get; init; }
        public int Page { get; init; }
        public int Size { get; init; }
        public int TotalPages => ( int )Math.Ceiling( ( double )Total / Size );
    }
}