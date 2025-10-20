using MovieApp.Application.Interfaces;

namespace MovieApp.Infrastructure.Readers
{
    public class XmlFileReader : IFileReader
    {
        public Task<List<T>> ReadAsync<T>(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
