using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Infrastructure.Configuration;
using MovieApp.Infrastructure.Readers;
using System.Text.Json;

namespace MovieApp.Infrastructure.Loaders
{
    public class MovieLoader : ILoader<MovieDto>
    {
        private readonly IFileReader _reader;
        private readonly DataFileOptions _options;
        private readonly ILogger<MovieLoader> _logger;

        public MovieLoader(IFileReader reader, IOptions<DataFileOptions> options, ILogger<MovieLoader> logger)
        {
            _reader = reader;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<List<MovieDto>> LoadAsync()
        {
            try
            {
                var result = await _reader.ReadAsync<MovieDto>(_options.MoviesFilePath);

                if (result == null || result.Count == 0)
                {
                    _logger.LogWarning($"MovieLoader: No movies were loaded from {_options.MoviesFilePath}");
                }
                else
                {
                    _logger.LogInformation($"MovieLoader: Loaded {result.Count} movies from {_options.MoviesFilePath}");
                }   

                return result ?? new List<MovieDto>();

            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, $"Movies file not found at path: {_options.MoviesFilePath}");
                return new List<MovieDto>();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Invalid JSON structure in {_options.MoviesFilePath}. Check file format.");
                return new List<MovieDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error while loading movies from {_options.MoviesFilePath}");
                return new List<MovieDto>();
            }



        }

    }
}
