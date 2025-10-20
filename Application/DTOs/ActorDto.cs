using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Application.DTOs
{
    public record ActorDto(int Id, string Name, DateOnly BirthDate);
}
