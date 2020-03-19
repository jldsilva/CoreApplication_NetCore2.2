using Core.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Data.Repositories
{
	public class BaseRepository<TDbContext, TEntity> : IDisposable, IBaseRepository<TEntity> where TDbContext : DbContext where TEntity : class
	{
		private readonly TDbContext _dbContext;
		private readonly DbSet<TEntity> _dbSet;
		private bool disposed = false;

		public BaseRepository(TDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<TEntity>();
		}

		public virtual TEntity Add(TEntity entity)
		{
			_dbSet.Add(entity);
			_dbContext.SaveChanges();

			return entity;
		}

		public virtual void Remove(TEntity entity)
		{
			if (_dbContext.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}

			_dbSet.Remove(entity);
			_dbContext.SaveChanges();
		}

		public virtual TEntity Select(int id)
		{
			return _dbSet.Find(id);
		}

		public virtual IQueryable<TEntity> Select()
		{
			return _dbSet?.AsQueryable();
		}

		public virtual TEntity Update(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			_dbContext.SaveChanges();

			return entity;
		}

		public virtual IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Where(predicate).AsEnumerable();
		}

		public virtual bool GetStatus()
		{
			try
			{
				_dbContext.Database.OpenConnection();
				_dbContext.Database.CloseConnection();
				return true;
			}
			catch
			{
				return false;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
			}

			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
