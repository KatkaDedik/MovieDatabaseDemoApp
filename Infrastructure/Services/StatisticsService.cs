using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Extensions;
using System.Linq;

namespace MovieApp.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public StatisticsService(IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public async Task<StatisticsResult> GetStatisticsAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            var actors = await _actorRepository.GetAllAsync();


            int totalMovies = movies.Count();

            int totalActors = actors.Count();

            decimal averageRating = 0;

            List<string> topGenres = new();
            int oldestActorAge = 0;
            int youngestActorAge = 0;
            string mostProlificActor = "";
            decimal highestRatedGenreAvg = StatisticsCalculations.GetHighestRatedGenreAvg(movies);
            List<string> TopGenres = new List<string>();
            int OldestActorAge = StatisticsCalculations.GetOldestActorAge(actors);
            int YoungestActorAge = StatisticsCalculations.GetYoungestActorAge(actors);
            string MostProlificActor = string.Empty;
            decimal HighestRatedGenreAvg = 0;
            Movie? longestTitleMovie = null;

            return new StatisticsResult(
                totalMovies,
                totalActors,
                averageRating,
                topGenres,
                oldestActorAge,
                youngestActorAge,
                mostProlificActor,
                highestRatedGenreAvg,
                longestTitleMovie
            );
        }
    }


    public static class StatisticsCalculations
    {
        /// <summary>
        /// Calculates the genre with the highest average movie rating and returns that average rating.
        /// </summary>
        /// <param name="movies"></param>
        /// <returns>The highest average rating as a decimal value.</returns>
        public static decimal GetHighestRatedGenreAvg(List<Movie> movies)
        {
            var highestRatedGenre = movies
                .GroupBy(m => m.Genre)
                .Select(g => new
                {
                    Genre = g.Key,
                    AverageRating = g.Average(m => m.Rating)
                })
                .OrderByDescending(x => x.AverageRating)
                .FirstOrDefault();
            return highestRatedGenre is null ? 0m : (decimal)highestRatedGenre.AverageRating;
        }

        public static decimal GetAverageRating(List<Movie> movies) { throw new NotImplementedException(); }

        public static int GetOldestActorAge(List<Actor> actors)
        {
            var oldestActorEntry = actors.OrderBy(a => a.BirthDate).FirstOrDefault();
            return oldestActorEntry is not null ? oldestActorEntry.BirthDate.GetAge() : 0;
        }

        public static int GetYoungestActorAge(List<Actor> actors)
        {
            var oldestActorEntry = actors.OrderByDescending(a => a.BirthDate).FirstOrDefault();
            return oldestActorEntry is not null ? oldestActorEntry.BirthDate.GetAge() : 0;
        }
    }
}
