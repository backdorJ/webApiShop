namespace WebApiShop.Data.Interfaces;

public interface IBaseRepository<T>
{
    Task<bool> CreateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<bool> IsExistAsync(int id);
    Task<bool> SaveAsync();
}