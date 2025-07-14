using BuyZone.Domain.BaseEntity;

namespace BuyZone.Domain;

public interface IRepository
{
    Task<T?> GetByIdAsync<T>(Guid id) where T:class,IBaseEntity;
    Task<bool> SaveChangesAsync();
    IQueryable<T> Query<T>() where T:class ,IBaseEntity ;
    IQueryable<T> TrackingQuery<T>()where T:class,IBaseEntity;
    Task AddAsync<T>(T entity) where T : class, IBaseEntity;
    void Update<T>(T entity) where T : class, IBaseEntity;
    void Delete<T>(T entity) where T : class, IBaseEntity;

}