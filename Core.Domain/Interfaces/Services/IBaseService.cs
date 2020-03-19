using System.Collections.Generic;

namespace Core.Domain.Interfaces.Services
{
	public interface IBaseService<TEntity> where TEntity : class
	{
		TEntity Add(TEntity entity);
		void Remove(int id);
		void Remove(TEntity entity);
		TEntity Select(int id);
		IEnumerable<TEntity> Select();
		TEntity Update(TEntity entity);
		void Dispose();
	}
}
