﻿using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected DataContext _context = context;
    private DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async virtual Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null) return null!;
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} entity :: {ex.Message}");
            return null!;
        }
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
        return entity ?? null!;
    }

    public async virtual Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
    {
        if (predicate == null) return null!;
        if (updatedEntity == null) return null!;
        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(predicate);
            if (existingEntity == null) return null!;

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return existingEntity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating {nameof(TEntity)} entity :: {ex.Message}");
            return null!;
        }
    }

    public async virtual Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) return false;

        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(predicate);
            if (existingEntity == null) return false;
           
            _dbSet.Remove(existingEntity);
            await _context.SaveChangesAsync();
            return true;
           
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)} entity :: {ex.Message}");
            return false;
        }
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
}
