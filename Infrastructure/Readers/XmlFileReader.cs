using MovieApp.Application.Interfaces;

namespace MovieApp.Infrastructure.Readers
{
    internal class XmlFileReader : IFileReader
    {
        public Task<List<T>> ReadAsync<T>(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
