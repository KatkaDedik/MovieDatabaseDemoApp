using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Configuration
{
    internal class DataFileOptions
    {
        public const string SectionName = "DataFiles";
        public string MoviesJson { get; set; } = string.Empty;
        public string ActorsXml { get; set; } = string.Empty;
    }
}
