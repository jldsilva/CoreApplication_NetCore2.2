using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Api.Interfaces
{
	public interface IBaseApiController<TEntity, TEntityViewModel> where TEntity : class where TEntityViewModel : class
	{
		Task<IActionResult> Add(TEntityViewModel viewModel);
		Task<IActionResult> Remove(int id);
		Task<IActionResult> Select(int id);
		Task<IActionResult> Select();
		Task<IActionResult> Update(int id, TEntityViewModel viewModel);
	}
}
