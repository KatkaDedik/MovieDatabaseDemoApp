namespace MovieApp.Application.Interfaces
{
    public interface ILoader<T>
    {
        Task<List<T>> LoadAsync();
    }
}
