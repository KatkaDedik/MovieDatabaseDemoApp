using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Application.Interfaces
{
    public interface IFileReader
    {
        Task<List<T>> ReadAsync<T>(string filePath);
    }
}
