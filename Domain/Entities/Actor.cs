namespace MovieApp.Domain.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
    }
}
