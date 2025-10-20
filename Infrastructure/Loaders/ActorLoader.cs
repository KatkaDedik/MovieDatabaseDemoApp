using Microsoft.Extensions.Options;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;
using MovieApp.Infrastructure.Configuration;

namespace MovieApp.Infrastructure.Loaders
{
    public class ActorLoader : ILoader<ActorDto>
    {
        private readonly IFileReader _reader;
        private readonly DataFileOptions _options;

        public ActorLoader(IFileReader reader, IOptions<DataFileOptions> options)
        {
            _reader = reader;
            _options = options.Value;
        }

        public async Task<List<ActorDto>> LoadAsync()
        {
            return await _reader.ReadAsync<ActorDto>(_options.ActorsFilePath);
        }
    }
}
