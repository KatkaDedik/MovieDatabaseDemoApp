using Microsoft.Extensions.Options;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Infrastructure.Configuration;

namespace MovieApp.Infrastructure.Loaders
{
    public class MovieLoader : ILoader<MovieDto>
    {
        private readonly IFileReader _reader;
        private readonly DataFileOptions _options;

        public MovieLoader(IFileReader reader, IOptions<DataFileOptions> options)
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
