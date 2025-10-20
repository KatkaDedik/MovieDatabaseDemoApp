using MovieApp.Application.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Infrastructure.Configuration;

namespace MovieApp.Infrastructure.Loaders
{
    public class ActorLoader : ILoader<Actor>
    {
        private readonly IFileReader _reader;
        private readonly DataFileOptions _options;

        public ActorLoader(IFileReader reader, DataFileOptions options)
        {
            _reader = reader;
            _options = options;
        }

        public async Task<List<Actor>> LoadAsync()
        {
            return await _reader.ReadAsync<Actor>(_options.ActorsFilePath);
        }
    }
}
