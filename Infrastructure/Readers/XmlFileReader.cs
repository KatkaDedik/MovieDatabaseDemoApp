using MovieApp.Application.Interfaces;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace MovieApp.Infrastructure.Readers
{
    public class XmlFileReader : IFileReader
    {
        private readonly ILogger<XmlFileReader> _logger;
        public XmlFileReader(ILogger<XmlFileReader> logger)
        {
            _logger = logger;
        }

        public async Task<List<T>> ReadAsync<T>(string filePath)
        {

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"XmlFileReader: File not found: {filePath}");
                }

                await using var stream = File.OpenRead(filePath);
                var serializer = new XmlSerializer(typeof(List<T>));
                return (List<T>)(serializer.Deserialize(stream) ?? new List<T>());
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"XmlFileReader: Invalid XML format in {filePath}");
                return new List<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"XMlFileReader: Unexpected error while reading {filePath}");
                return new List<T>();
            }
        }
    }
}
