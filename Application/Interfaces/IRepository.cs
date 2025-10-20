using MovieApp.Domain.Entities;

namespace MovieApp.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task AddRangeAsync(IEnumerable<T> entities);
        public Task<List<T>> GetAllAsync();
    }
}
