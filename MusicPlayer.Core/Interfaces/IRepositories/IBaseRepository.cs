namespace MusicPlayer.Core.Interfaces.IRepositories;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(int page = 0, int pageSize = 0);
    Task<T> GetByIdAsync<Tid>(Tid id);
    Task<T> CreateAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
    Task SaveChangeAsync();
}
