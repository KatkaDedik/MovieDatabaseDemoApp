using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Infrastructure.Configuration;

namespace MovieApp.Infrastructure.Loaders
{
    public class MovieLoader : ILoader<Movie>
    {
        private readonly IFileReader _reader;
        private readonly DataFileOptions _options;

        public MovieLoader(IFileReader reader, DataFileOptions options)
        {
            _reader = reader;
            _options = options;
        }

        public async Task<List<Movie>> LoadAsync()
        {
            return await _reader.ReadAsync<Movie>(_options.MoviesFilePath);
        }

    }
}
