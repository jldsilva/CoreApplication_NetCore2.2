using Core.Domain.Interfaces.Repositories;
using Core.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Core.Domain.Services
{
	public class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
	{
		private readonly IBaseRepository<TEntity> _repository;
		private bool disposed = false;

		public BaseService(IBaseRepository<TEntity> repository)
		{
			_repository = repository;
		}

		public virtual TEntity Add(TEntity entity)
		{
			return _repository.Add(entity);
		}

		public virtual void Remove(int id)
		{
			_repository.Remove(_repository.Select(id));
		}

		public virtual void Remove(TEntity entity)
		{
			_repository.Remove(entity);
		}

		public virtual TEntity Select(int id)
		{
			return _repository.Select(id);
		}

		public virtual IEnumerable<TEntity> Select()
		{
			return _repository.Select();
		}

		public virtual TEntity Update(TEntity entity)
		{
			return _repository.Update(entity);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_repository.Dispose();
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
