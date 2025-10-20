using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;

namespace MovieApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Actor> Actors => Set<Actor>();
    }
}
