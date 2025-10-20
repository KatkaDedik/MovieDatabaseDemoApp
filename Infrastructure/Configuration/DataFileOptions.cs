using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Configuration
{
    public class DataFileOptions
    {
        public const string SectionName = "DataFiles";
        public string MoviesFilePath { get; set; } = string.Empty;
        public string ActorsFilePath { get; set; } = string.Empty;
    }
}
