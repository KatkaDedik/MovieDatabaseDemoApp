namespace MovieApp.Application.Interfaces
{
    public interface IFileReader
    {
        Task<List<T>> ReadAsync<T>(string filePath);
    }
}
