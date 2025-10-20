using Xunit;
using MovieApp.Infrastructure.Readers;
using System.Xml.Serialization;

public class XmlFileReaderTests
{
    [Fact]
    public async Task ReadAsync_ShouldReturnParsedList()
    {
        var filePath = Path.GetTempFileName();
        var sampleData = new List<TestActor>
        {
            new() { Name = "Leonardo DiCaprio" },
            new() { Name = "Keanu Reeves" }
        };

        var serializer = new XmlSerializer(typeof(List<TestActor>));
        await using (var stream = File.Create(filePath))
        {
            serializer.Serialize(stream, sampleData);
        }

        var reader = new XmlFileReader();

        var result = await reader.ReadAsync<TestActor>(filePath);

        Assert.Equal(2, result.Count);
        Assert.Equal("Leonardo DiCaprio", result[0].Name);

        File.Delete(filePath);
    }

    public class TestActor
    {
        public string Name { get; set; } = string.Empty;
    }
}
