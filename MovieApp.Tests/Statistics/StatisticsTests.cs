using Xunit;
using MovieApp.Tests.TestData;
using MovieApp.Application.Services;

namespace MovieApp.Tests.Statistics
{
    public class StatisticsTests
    {
        [Fact]
        public void GetAverageRatingTest()
        {
            var movies = SampleData.GetMovies(SampleData.GetActors());
            var averageRating = StatisticsCalculations.GetAverageMovieRating(movies);
            Assert.Equal(9.075m, (decimal)averageRating);
        }

        [Fact]
        public void GetLongestTitleMovieTest()
        {
            var movies = SampleData.GetMovies(SampleData.GetActors());
            var longestTitleMovie = StatisticsCalculations.GetLongestTitleMovie(movies);
            Assert.Equal("The Shawshank Redemption", longestTitleMovie.Title);
        }

        [Fact]
        public void GetTopGenresTest()
        {
            var movies = SampleData.GetMovies(SampleData.GetActors());
            var topGenres = StatisticsCalculations.GetTopGenres(movies, 1);
            Assert.Equal(new List<string> { "Drama" }, topGenres);
        }

        [Fact]
        public void GetOldestActorAgeTest()
        {
            var actors = SampleData.GetActors();
            var oldestAge = StatisticsCalculations.GetOldestActorAge(actors);
            Assert.Equal(DateTime.Now.Year - 1937, oldestAge);
        }

        [Fact]
        public void GetYoungestActorAgeTest()
        {
            var actors = SampleData.GetActors();
            var youngestAge = StatisticsCalculations.GetYoungestActorAge(actors);
            Assert.Equal(DateTime.Now.Year - 1979, youngestAge);
        }

        [Fact]
        public void GetMostProlificActorTest()
        {
            var movies = SampleData.GetMovies(SampleData.GetActors());
            var mostProlificActor = StatisticsCalculations.GetMostProlificActor(movies);
            Assert.Equal("Morgan Freeman", mostProlificActor);
        }

        [Fact]
        public void GetHighestRatedGenreAvgTest()
        {
            var movies = SampleData.GetMovies(SampleData.GetActors());
            var highestRatedGenreAvg = StatisticsCalculations.GetHighestRatedGenreAvg(movies);
            //should be Crime with avg 9.2... drama is 9.05, action is 9.0
            Assert.Equal(9.2m, highestRatedGenreAvg);
        }
    }
}
