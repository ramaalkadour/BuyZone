using BuyZone.Domain.BaseEntity;

namespace BuyZone.Domain;

public interface IRepository
{
    Task<T?> GetByIdAsync<T>(Guid id) where T:class,IBaseEntity;
    Task<bool> SaveChangesAsync<T>()where T:class,IBaseEntity;
    IQueryable<T> Query<T>() where T:class ,IBaseEntity ;
    IQueryable<T> TrackingQuery<T>()where T:class,IBaseEntity;

}