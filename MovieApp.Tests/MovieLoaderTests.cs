using Xunit;
using Moq;
using MovieApp.Infrastructure.Loaders;
using MovieApp.Application.Interfaces;
using MovieApp.Application.DTOs;
using MovieApp.Domain.Entities;
using MovieApp.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

public class MovieLoaderTests
{
    [Fact]
    public async Task LoadAsync_ReturnSingleMovieFromReader()
    {
        var mockReader = new Mock<IFileReader>();

        var fakeMovies = new List<MovieDto>
        {
            new MovieDto(1, "Imaginarium", "Drama", 9.3f, 1994, new List<int> { 1, 2, 3 }),
        };


        mockReader
            .Setup(r => r.ReadAsync<MovieDto>(It.IsAny<string>()))
            .ReturnsAsync(fakeMovies);

        var options = Options.Create(new DataFileOptions { MoviesFilePath = "fake" });

        var movieLoader = new MovieLoader(mockReader.Object, options);

        List<MovieDto> result = await movieLoader.LoadAsync();

        Assert.Single(result);
        Assert.Equal("Imaginarium", result[0].Title);
    }

    [Fact]
    public async Task LoadAsync_ReturnMultipleMoviesFromReader()
    {
        var mockReader = new Mock<IFileReader>();

        var fakeMovies = new List<MovieDto>
        {
            new MovieDto(1, "The Shawshank Redemption", "Drama", 9.3f, 1994, new List<int> { 1, 2, 3 }),
            new MovieDto(2, "The Godfather", "Crime", 9.2f, 1972, new List<int> { 4, 5, 6 })
        };

        mockReader
            .Setup(r => r.ReadAsync<MovieDto>(It.IsAny<string>()))
            .ReturnsAsync(fakeMovies);

        var options = Options.Create(new DataFileOptions { MoviesFilePath = "fake" });

        var movieLoader = new MovieLoader(mockReader.Object, options);

        List<MovieDto> result = await movieLoader.LoadAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal("The Shawshank Redemption", result[0].Title);
        Assert.Equal("Crime", result[1].Genre);
    }

}
