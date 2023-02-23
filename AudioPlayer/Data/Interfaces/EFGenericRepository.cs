#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AudioPlayer.Data.Domain.Interfaces;
using AudioPlayer.Models;
using System.Data.Entity;

namespace AudioPlayer.Data.Interfaces;

public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    protected EFGenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    protected IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        if (includes is { Length: > 0 })
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }
        
        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return query;
    }

    public virtual TEntity GetById(object? id)
    {
        return id is null ? null! : _dbSet.Find(id)!;
    }
    
    public virtual Task<TEntity> GetByIdAsync(object id)
    {
        return _dbSet.FindAsync(id);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return GetQueryable().AsEnumerable();
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includes, skip, take).ToList();
    }
    
    public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includes, skip, take).ToListAsync();
    }

    public int GetCount(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return GetQueryable(predicate).Count();
    }

    public TEntity Create()
    {
        return _dbSet.Create();
    }

    public TEntity Add(TEntity entity)
    {
        return _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public void DeleteRange(TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
        }

        _dbSet.RemoveRange(entities);
    }

    public void RefreshAll()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries<TEntity>())
        {
            entry.Reload();
        }
    }

    public TEntity Clone(TEntity entity)
    {
        var clone = Create();
        Add(clone);
        var originalEntityValues = _dbContext.Entry(entity).CurrentValues;
        _dbContext.Entry(clone).CurrentValues.SetValues(originalEntityValues);

        return clone;
    }
}