using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalHomework.Application.DTOs
{
    public record MovieDto(int Id, string Title, string Genre, float Rating, int Year, List<int> ActorsIds);
}