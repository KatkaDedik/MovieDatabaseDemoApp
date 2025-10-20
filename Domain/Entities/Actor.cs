namespace MovieApp.Domain.Entities
{
    public class Actor
    {
        int Id { get; set; }
        string Name { get; set; } = string.Empty;
        DateOnly BirthDate { get; set; }
    }
}
