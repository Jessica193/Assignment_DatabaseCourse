using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SQLitePCL;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction _transaction = null!;

    #region Transaction Management

    public virtual async Task BeginTransactionAsync()
    {
        if (_transaction == null)
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
    }

    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    #endregion



    #region CRUD
    public async virtual Task<bool> CreateAsync(TEntity entity)
    {
        if (entity == null) return false!;
        await _dbSet.AddAsync(entity);
        return true;
    }


    public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    public async virtual Task<IEnumerable<TEntity>> GetAllWithDetailsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression)
    {
        IQueryable<TEntity> query = _dbSet;
        if(includeExpression != null)
        {
            query = includeExpression(query);
        }
        return await query.ToListAsync();
    }

    public async virtual Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) return null!;

        var entity = await _dbSet.FirstOrDefaultAsync(predicate);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Modified; // Ser till att entiteten spåras. genererad av chatGPT4o
        }
        return entity ?? null!;
    }

    public async virtual Task<TEntity> GetOneWithDetailsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression, Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) return null!;

        IQueryable<TEntity> query = _dbSet;
        if (includeExpression != null)
        {
            query = includeExpression(query);
        }
        var entity = await query.FirstOrDefaultAsync(predicate);

        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Modified; 
        }

        return entity ?? null!;
    }


    /// <summary>
    /// _context.Entry(updatedEntity).State = EntityState.Modified; genererad av chatGPT4o
    /// Används istället för _dbSet.Update då entiteten är spårad av GetOne-metoden. Koden markerar entiteten som ändrad och
    /// sparar ändringarna i nästa steg med _context.SaveChangesAsync();.
    /// </summary>
    /// <param name="updatedEntity"></param>
    /// <returns></returns>
    public virtual bool Update(TEntity updatedEntity)
    {
        if (updatedEntity == null) return false;
        _context.Entry(updatedEntity).State = EntityState.Modified;
        return true;
    }

    public virtual bool Delete(TEntity entity)
    {
        if (entity == null) return false;
        _dbSet.Remove(entity);
        return true;
    }

    public async virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) return false;
        try
        {
            var result = await _dbSet.AnyAsync(predicate);
            if (result)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding {nameof(TEntity)} entity :: {ex.Message}");
            return false;
        }
    }

    public virtual async Task SaveToDatabaseAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion
}
