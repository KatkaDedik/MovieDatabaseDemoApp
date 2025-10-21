namespace MovieApp.Application.DTOs
{
    public record MovieDto(int Id, string Title, string Genre, float Rating, int Year, List<int> ActorIds);
}