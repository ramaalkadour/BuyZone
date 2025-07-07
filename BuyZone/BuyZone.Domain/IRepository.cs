namespace BuyZone.Domain;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task <List<T>> GetAllAsync();
    Task<bool> SaveChangesAsync();
    
}