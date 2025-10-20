using MovieApp.Application.Interfaces;
using System.Xml.Serialization;

namespace MovieApp.Infrastructure.Readers
{
    public class XmlFileReader : IFileReader
    {
        public async Task<List<T>> ReadAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            await using var stream = File.OpenRead(filePath);
            var serializer = new XmlSerializer(typeof(List<T>));
            return (List<T>)(serializer.Deserialize(stream) ?? new List<T>());

        }
    }
}
