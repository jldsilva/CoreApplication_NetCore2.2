using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Domain.Interfaces.Repositories
{
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		TEntity Add(TEntity entity);
		void Remove(TEntity entity);
		TEntity Select(int id);
		IQueryable<TEntity> Select();
		TEntity Update(TEntity entity);
		IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
		void Dispose();
	}
}
