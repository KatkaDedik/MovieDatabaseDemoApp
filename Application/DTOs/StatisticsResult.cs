using MovieApp.Domain.Entities;

namespace MovieApp.Application.DTOs
{
    public record StatisticsResult(
        int TotalMovies,
        int TotalActors,
        decimal AverageRating,
        List<string> TopGenres,
        int OldestActorAge,
        int YoungestActorAge,
        string MostProlificActor,
        decimal HighestRatedGenreAvg,
        Movie? LongestTitleMovie
        );
}
