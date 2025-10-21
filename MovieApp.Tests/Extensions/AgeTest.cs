using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Infrastructure.Configuration;
using MovieApp.Infrastructure.Loaders;
using MovieApp.Infrastructure.Readers;
using Xunit;

public class AgeTest
{
    [Fact]
    public async Task GetAgeTest()
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

        var mockLogger = new Mock<ILogger<MovieLoader>>();
        var movieLoader = new MovieLoader(mockReader.Object, options, mockLogger.Object);

        List<MovieDto> result = await movieLoader.LoadAsync();

        Assert.Single(result);
        Assert.Equal("Imaginarium", result[0].Title);
    }
}
