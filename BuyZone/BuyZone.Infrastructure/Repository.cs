using BuyZone.Domain;
using BuyZone.Domain.BaseEntity;
using BuyZone.Infrastructure.DbContest;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Infrastructure;

public class Repository:IRepository
{
    private readonly BuyZoneDbContext _context;

    public Repository(BuyZoneDbContext context)
    {
        _context = context;
    }

 

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<T?> GetByIdAsync<T>(Guid id) where T :class, IBaseEntity
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync<T>() where T : class,IBaseEntity
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Query<T>() where T :class, IBaseEntity
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> TrackingQuery<T>() where T :class, IBaseEntity
    {
        return _context.Set<T>().AsTracking();
    }

    public async Task AddAsync<T>(T entity) where T : class, IBaseEntity
    {
        await _context.Set<T>().AddAsync(entity);
    }
}
