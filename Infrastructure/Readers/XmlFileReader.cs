using Microsoft.Extensions.Logging;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using System.Text.Json;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

                if (stream.Length == 0)
                {
                    _logger.LogError($"XML file {filePath} is empty.");
                    return new List<T>();
                }

                stream.Position = 0;

                //TODO fix deserialization - currently needs to know Root Attribute... but I cant anymore deal with this 
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute("Actors"));
                List<T>? result = (List<T>?)serializer.Deserialize(stream);

                return result ?? new List<T>();

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
