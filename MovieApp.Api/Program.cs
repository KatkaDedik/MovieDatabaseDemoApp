using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Infrastructure;
using MovieApp.Infrastructure.Data;
using MovieApp.Infrastructure.Loaders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialize Db
using (var scope = app.Services.CreateScope())
{
    var servicesProvider = scope.ServiceProvider;
    var db = servicesProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    db.Database.EnsureCreated();

    var movieRepo = scope.ServiceProvider.GetRequiredService<IMovieRepository>();
    var actorRepo = scope.ServiceProvider.GetRequiredService<IActorRepository>();

    var movieLoader = scope.ServiceProvider.GetRequiredService<ILoader<MovieDto>>();
    var actorLoader = scope.ServiceProvider.GetRequiredService<ILoader<ActorDto>>();

    bool actorsSeeded = false;
    bool moviesSeeded = false;

    if (!db.Actors.Any())
    {
        try
        {
            List<ActorDto> actorDtos = await actorLoader.LoadAsync();
            if (actorDtos.Any())
            {
                var actors = actorDtos.Select(a => new Actor
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthDate = DateOnly.FromDateTime(a.BirthDate)
                }).ToList();

                await actorRepo.AddRangeAsync(actors);
                actorsSeeded = true;

                logger.LogInformation($"Seeded {actors.Count} actors.");
            }
            else
            {
                logger.LogWarning("No actors loaded from XML.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to seed actors.");
        }
    }
    else
    {
        logger.LogInformation("Actors have already existed, skipping seeding.");
    }


    if (!db.Movies.Any())
    {
        try
        {
            var movieDtos = await movieLoader.LoadAsync();

            if (movieDtos.Any())
            {
                var existingActors = await actorRepo.GetAllAsync();

                var movies = movieDtos.Select(m => new Movie
                {
                    Title = m.Title,
                    Genre = m.Genre,
                    Rating = m.Rating,
                    Year = m.Year,
                    Actors = existingActors
                .Where(a => m.ActorIds.Contains(a.Id))
                .ToList()
                }).ToList();

                await movieRepo.AddRangeAsync(movies);
                moviesSeeded = true;
                logger.LogInformation($"Seeded {movies.Count} movies.");
            }
            else
            {
                logger.LogWarning("No movies loaded from JSON.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to seed movies.");
        }
    }
    else
    {
        logger.LogInformation("Movies have already existed, skipping seeding.");
    }

    if (!db.Set<Movie>().Include(m => m.Actors).Any(m => m.Actors.Any()))
    {
        logger.LogInformation("Trying to Re-Join existing movies and actors");

        var movieDtos = await movieLoader.LoadAsync();
        var existingMovies = await movieRepo.GetAllAsync();
        var existingActors = await actorRepo.GetAllAsync();

        int relationCount = 0;

        foreach (var movieDto in movieDtos)
        {
            try
            {
                var movie = existingMovies.FirstOrDefault(m => m.Id == movieDto.Id);
                if (movie == null) continue;

                var relatedActors = existingActors
                    .Where(a => movieDto.ActorIds.Contains(a.Id))
                    .ToList();

                foreach (var actor in relatedActors)
                {
                    if (!movie.Actors.Contains(actor))
                    {
                        movie.Actors.Add(actor);
                        relationCount++;
                    }
                }
            }
            catch(NullReferenceException nex)
            {
                logger.LogError(nex, $"Null reference while re-joining actors for movie ID {movieDto.Id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to re-join actors for movie ID {movieDto.Id}");
            }
        }
        await db.SaveChangesAsync();
        logger.LogInformation("Join existing movies and actors finished");
    }

}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
