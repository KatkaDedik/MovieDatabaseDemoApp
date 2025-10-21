using MovieApp.Domain.Entities;
using MovieApp.Application.DTOs;

namespace MovieApp.Application.Interfaces
{
    public interface IStatisticsService
    {
        Task<StatisticsResult> GetStatisticsAsync();
    }
}
