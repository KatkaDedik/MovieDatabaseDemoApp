using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application.DTOs;
using MovieApp.Infrastructure.Configuration;
using MovieApp.Infrastructure.Data;
using MovieApp.Infrastructure.Loaders;
using MovieApp.Infrastructure.Readers;
using MovieApp.Infrastructure.Repositories;

namespace MovieApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataFileOptions>(configuration.GetSection(DataFileOptions.SectionName));

            services.AddDbContext<AppDbContext>(Options => Options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            //services.AddSingleton<IFileReader, JsonFileReader>();
            //services.AddSingleton<ILoader<MovieDto>, MovieLoader>();
            //services.AddSingleton<ILoader<ActorDto>, ActorLoader>();

            //services.AddScoped<IMovieRepository, MovieRepository>();
            //services.AddScoped<IActorRepository, ActorRepository>();

            return services;
        }
    }
}
