using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Domain.Extensions;
using System.Collections.Generic;
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
            decimal averageRating = StatisticsCalculations.GetAverageMovieRating(movies);
            List<string> topGenres = StatisticsCalculations.GetTopGenres(movies, 3);
            int oldestActorAge = StatisticsCalculations.GetOldestActorAge(actors);
            int youngestActorAge = StatisticsCalculations.GetYoungestActorAge(actors);
            string mostProlificActor = StatisticsCalculations.GetMostProlificActor(movies);
            decimal highestRatedGenreAvg = StatisticsCalculations.GetHighestRatedGenreAvg(movies);
            Movie? longestTitleMovie = StatisticsCalculations.GetLongestTitleMovie(movies);

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
        public static Movie? GetLongestTitleMovie(List<Movie> movies)
        {
            return movies
                .OrderByDescending(m => m.Title.Length)
                .FirstOrDefault();
        }

        public static string GetMostProlificActor(List<Movie> movies)
        {
            return movies
                .SelectMany(m => m.Actors)
                .GroupBy(a => a.Name)
                .Select(g => new
                {
                    ActorName = g.Key,
                    AppearanceCount = g.Count()
                })
                .OrderByDescending(x => x.AppearanceCount)
                .FirstOrDefault()?.ActorName ?? "Katka";
        }

        /// <summary>
        /// The top N most frequent genres.
        /// </summary>
        /// <param name="movies">List of movies</param>
        /// <param name="topN">top N movies will be returned</param>
        /// <returns></returns>
        public static List<string> GetTopGenres(List<Movie> movies, int topN)
        {
            return movies
                .GroupBy(m => m.Genre)
                .OrderByDescending(g => g.Count())
                .Take(topN)
                .Select(g => g.Key)
                .ToList();
        }

        /// <summary>
        /// Calculates the average rating of a list of movies.
        /// </summary>
        /// <param name="movies">A list of <see cref="Movie"/> objects for which the average rating is calculated.  The list must not be
        /// null, but it can be empty.</param>
        /// <returns>The average rating of the movies as a <see cref="decimal"/>.  Returns 0 if the list is null or contains no
        /// movies.</returns>
        public static decimal GetAverageMovieRating(List<Movie> movies)
        {
            if (movies == null || movies.Count == 0)
            {
                return 0m;
            }
            return (decimal)movies.Average(m => m.Rating);
        }

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
