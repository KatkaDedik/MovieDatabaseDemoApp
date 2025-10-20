using MovieApp.Domain.Entities;

namespace MovieApp.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<List<T>> GetAllAsync();
    }
}
