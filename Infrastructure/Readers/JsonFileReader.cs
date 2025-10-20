using MovieApp.Application.Interfaces;
using System.Text.Json;

namespace MovieApp.Infrastructure.Readers
{
    public class JsonFileReader : IFileReader
    {
        public async Task<List<T>> ReadAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var json = await File.ReadAllTextAsync(filePath);
            var result = JsonSerializer.Deserialize<List<T>>(json);

            return result ?? new List<T>();
        }
    }
}
