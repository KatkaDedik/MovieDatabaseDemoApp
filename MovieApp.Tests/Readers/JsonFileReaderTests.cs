using Microsoft.Extensions.Logging;
using Moq;
using MovieApp.Infrastructure.Readers;
using System.Text.Json;
using Xunit;

public class JsonFileReaderTests
{
    [Fact]
    public async Task ReadAsync_ReturnParsedList()
    {
        var filePath = Path.GetTempFileName();
        var sampleData = new[]
        {
            new { Title = "Inception", Year = 2010 },
            new { Title = "Matrix", Year = 1999 }
        };
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(sampleData));

        var mockLogger = new Mock<ILogger<JsonFileReader>>();
        var reader = new JsonFileReader(mockLogger.Object);

        List<TestMovie> result = await reader.ReadAsync<TestMovie>(filePath);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Inception", (string)result[0].Title);
        
        File.Delete(filePath);
    }

    public class TestMovie
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
