using MovieApp.Domain.Entities;

namespace MovieApp.Application.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<List<Movie>> GetTopRatedAsync(int count);
    }
}
