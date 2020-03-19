using System.Collections.Generic;

namespace Core.Application.Interfaces
{
	public interface IBaseAppService<TEntity, TEntityViewModel> where TEntity : class where TEntityViewModel : class
	{
		TEntityViewModel Add(TEntityViewModel viewModel);
		void Remove(int id);
		void Remove(TEntityViewModel viewModel);
		TEntityViewModel Select(int id);
		IEnumerable<TEntityViewModel> Select();
		TEntityViewModel Update(TEntityViewModel viewModel);
		TEntityViewModel Update(int id, TEntityViewModel viewModel);
		void Dispose();
	}
}
