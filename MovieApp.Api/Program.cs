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
builder.Services.AddSwaggerGen();
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

    if(!db.Actors.Any())
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
                var allActors = await db.Actors.ToListAsync();

                var movies = movieDtos.Select(m => new Movie
                {
                    Title = m.Title,
                    Genre = m.Genre,
                    Rating = m.Rating,
                    Year = m.Year,
                    Actors = allActors.Where(a => m.ActorsIds.Contains(a.Id)).ToList()
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


}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
