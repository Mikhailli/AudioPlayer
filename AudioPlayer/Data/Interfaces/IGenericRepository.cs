#nullable enable
using System.Linq.Expressions;
using AudioPlayer.Data.Domain.Interfaces;

namespace AudioPlayer.Data.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    TEntity GetById(object? id);
    
    Task<TEntity> GetByIdAsync(object id);
    
    IEnumerable<TEntity> GetAll();
    
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null);
    
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null);
    
    int GetCount(Expression<Func<TEntity, bool>>? predicate = null);

    TEntity Create();

    TEntity Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    void DeleteRange(TEntity[] entities);

    void RefreshAll();

    TEntity Clone(TEntity entity);
}