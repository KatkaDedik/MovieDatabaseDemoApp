using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Application.Services;
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

            services.AddOptions<DataFileOptions>()
                    .BindConfiguration("DataFiles");

            services.AddSingleton<JsonFileReader>();
            services.AddSingleton<XmlFileReader>();

            services.AddSingleton<ILoader<MovieDto>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<DataFileOptions>>();
                var reader = sp.GetRequiredService<JsonFileReader>();
                var logger = sp.GetRequiredService<ILogger<MovieLoader>>();
                return new MovieLoader(reader, options, logger);
            });

            services.AddSingleton<ILoader<ActorDto>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<DataFileOptions>>();
                var reader = sp.GetRequiredService<XmlFileReader>();
                var logger = sp.GetRequiredService<ILogger<ActorLoader>>();
                return new ActorLoader(reader, options, logger);
            });

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }
    }
}
