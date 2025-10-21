using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Infrastructure.Configuration;
using MovieApp.Infrastructure.Readers;

namespace MovieApp.Infrastructure.Loaders
{
    public class ActorLoader : ILoader<ActorDto>
    {
        private readonly IFileReader _reader;
        private readonly DataFileOptions _options;
        private readonly ILogger<ActorLoader> _logger;

        public ActorLoader(IFileReader reader, IOptions<DataFileOptions> options, ILogger<ActorLoader> logger)
        {
            _reader = reader;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<List<ActorDto>> LoadAsync()
        {
            try
            {
                var result = await _reader.ReadAsync<ActorDto>(_options.ActorsFilePath);

                if (result == null || result.Count == 0)
                {
                    _logger.LogWarning($"ActorLoader: No actors were loaded from {_options.ActorsFilePath}");
                }
                else
                {
                    _logger.LogInformation($"ActorLoader: Loaded {result.Count} actors from {_options.ActorsFilePath}");
                }

                return result ?? new List<ActorDto>();
            }
            catch (FileNotFoundException ex) {                 
                _logger.LogError(ex, $"Actors file not found at path: {_options.ActorsFilePath}");
                return new List<ActorDto>();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Invalid XML structure in {_options.ActorsFilePath}. Check file format." );
                return new List<ActorDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error while loading actors from {_options.ActorsFilePath}");
                return new List<ActorDto>();
            }
        }
    }
}
