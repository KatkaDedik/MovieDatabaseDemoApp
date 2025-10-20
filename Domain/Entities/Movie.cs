namespace MovieApp.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public float Rating { get; set; }
        public int Year { get; set; }
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}
