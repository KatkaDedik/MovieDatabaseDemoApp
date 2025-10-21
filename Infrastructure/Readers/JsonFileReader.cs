using Microsoft.Extensions.Logging;
using MovieApp.Application.Interfaces;
using System.Text.Json;
using System.Xml.Serialization;

namespace MovieApp.Infrastructure.Readers
{
    public class JsonFileReader : IFileReader
    {
        private readonly ILogger<JsonFileReader> _logger;

        public JsonFileReader(ILogger<JsonFileReader> logger)
        {
            _logger = logger;
        }
        public async Task<List<T>> ReadAsync<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    _logger.LogError($"JsonFileReader: File not found at {filePath}");
                    return new List<T>();
                }

                var json = await File.ReadAllTextAsync(filePath);
                var result = JsonSerializer.Deserialize<List<T>>(json);

                _logger.LogInformation($"JsonFileReader: Successfully loaded {result?.Count ?? 0} records from {filePath}");

                return result ?? new List<T>();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"JsonFileReader: Invalid XML format in {filePath}");
                return new List<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"JsonFileReader: Unexpected error while reading {filePath}");
                return new List<T>();
            }
        }
    }
}
