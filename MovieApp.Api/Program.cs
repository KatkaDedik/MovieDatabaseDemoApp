using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Interfaces;
using MovieApp.Infrastructure;
using MovieApp.Infrastructure.Data;
using MovieApp.Application.DTOs;
using MovieApp.Domain.Entities;

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

    db.Database.EnsureCreated();

    var movieRepo = scope.ServiceProvider.GetRequiredService<IMovieRepository>();
    var actorRepo = scope.ServiceProvider.GetRequiredService<IActorRepository>();


    if (!db.Movies.Any() || !db.Actors.Any())
    {
        var movieLoader = scope.ServiceProvider.GetRequiredService<ILoader<MovieDto>>();
        var actorLoader = scope.ServiceProvider.GetRequiredService<ILoader<ActorDto>>();

        List<MovieDto> movieDtos = await movieLoader.LoadAsync();
        List<ActorDto> actorDtos = await actorLoader.LoadAsync();

        var actors = actorDtos.Select(a => new MovieApp.Domain.Entities.Actor
        {
            Id = a.Id,
            Name = a.Name,
            BirthDate = a.BirthDate
        }).ToList();

        var movies = movieDtos.Select(m => new MovieApp.Domain.Entities.Movie
        {
            Id = m.Id,
            Title = m.Title,
            Genre = m.Genre,
            Rating = m.Rating,
            Year = m.Year,
            Actors = actors.Where(a => m.ActorsIds.Contains(a.Id)).ToList()
        }).ToList();

        await actorRepo.AddRangeAsync(actors);
        await movieRepo.AddRangeAsync(movies);

        Console.WriteLine("Data where loaded successfully!");
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
