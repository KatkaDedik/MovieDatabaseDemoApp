using MovieApp.Domain.Entities;
using System.Collections.Generic;

namespace MovieApp.Tests.TestData
{
    public static class SampleData
    {
        public static List<Actor> GetActors() => new()
        {
            new Actor { Id = 1, Name = "Morgan Freeman", BirthDate = new DateOnly(1937, 6, 1) },
            new Actor { Id = 2, Name = "Tim Robbins", BirthDate = new DateOnly(1958, 10, 16) },
            new Actor { Id = 3, Name = "Al Pacino", BirthDate = new DateOnly(1940, 4, 25) },
            new Actor { Id = 4, Name = "Christian Bale", BirthDate = new DateOnly(1974, 1, 30) },
            new Actor { Id = 5, Name = "Heath Ledger", BirthDate = new DateOnly(1979, 4, 4) },
            new Actor { Id = 6, Name = "Brad Pitt", BirthDate = new DateOnly(1963, 12, 18) },
        };

        public static List<Movie> GetMovies(List<Actor> actors)
        {
            var morgan = actors[0];
            var tim = actors[1];
            var al = actors[2];
            var bale = actors[3];
            var ledger = actors[4];
            var brad = actors[5];

            return new()
            {
                new Movie
                {
                    Id = 1,
                    Title = "The Shawshank Redemption",
                    Genre = "Drama",
                    Rating = 9.3f,
                    Year = 1994,
                    Actors = new List<Actor> { morgan, tim }
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Dark Knight",
                    Genre = "Action",
                    Rating = 9.0f,
                    Year = 2008,
                    Actors = new List<Actor> { bale, ledger, morgan }
                },
                new Movie
                {
                    Id = 3,
                    Title = "Fight Club",
                    Genre = "Drama",
                    Rating = 8.8f,
                    Year = 1999,
                    Actors = new List<Actor> { brad }
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Godfather",
                    Genre = "Crime",
                    Rating = 9.2f,
                    Year = 1972,
                    Actors = new List<Actor> { al }
                }
            };
        }
    }
}
