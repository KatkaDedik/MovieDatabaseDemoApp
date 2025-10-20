namespace MovieApp.Domain.Entities
{
    public class Movie
    {
        int Id { get; set; }
        string Title { get; set; } = string.Empty;
        string Genre { get; set; } = string.Empty;
        float Rating { get; set; }
        int Year { get; set; }
        List<int> ActorsIds { get; set; } = new();
    }
}
