using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using System.Linq;

namespace Application.Services
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

            // TODO: implement with LINQ
            int totalMovies = movies.Count;
            int totalActors = actors.Count;
            decimal averageRating = 0;
            List<string> topGenres = new();
            int oldestActorAge = 0;
            int youngestActorAge = 0;
            string mostProlificActor = "";
            decimal highestRatedGenreAvg = 0;
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
}
