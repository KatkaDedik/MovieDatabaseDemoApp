using Microsoft.Extensions.Options;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Infrastructure.Configuration;
using MovieApp.Infrastructure.Readers;

namespace MovieApp.Infrastructure.Loaders
{
    public class MovieLoader : ILoader<MovieDto>
    {
        private readonly JsonFileReader _reader;
        private readonly DataFileOptions _options;

        public MovieLoader(JsonFileReader reader, IOptions<DataFileOptions> options)
        {
            _reader = reader;
            _options = options.Value;
        }

        public async Task<List<MovieDto>> LoadAsync()
        {
            return await _reader.ReadAsync<MovieDto>(_options.MoviesFilePath);
        }

    }
}
