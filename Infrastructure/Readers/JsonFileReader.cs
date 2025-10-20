using MovieApp.Application.Interfaces;

namespace MovieApp.Infrastructure.Readers
{
    public class JsonFileReader : IFileReader
    {
        public Task<List<T>> ReadAsync<T>(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
