using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Infrastructure.Data;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Movie> entities)
        {
            await _context.Movies.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Actors)
                .ToListAsync();
        }

        public Task<List<Movie>> GetTopRatedAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}
