using Core.Api.Interfaces;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
	public class BaseApiController<TEntity, TEntityViewModel> : Controller, IBaseApiController<TEntity, TEntityViewModel> where TEntity : class where TEntityViewModel : class
	{
		private readonly IBaseAppService<TEntity, TEntityViewModel> _appService;

		public BaseApiController(IBaseAppService<TEntity, TEntityViewModel> appService)
		{
			_appService = appService;
		}

		[HttpPost]
		public virtual async Task<IActionResult> Add(TEntityViewModel viewModel)
		{
			IActionResult output;

			try
			{
				output = Ok(_appService.Add(viewModel));
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}

		[HttpDelete("{id}")]
		public virtual async Task<IActionResult> Remove(int id)
		{
			IActionResult output;

			try
			{
				_appService.Remove(id);
				output = Ok();
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}

		[HttpGet("{id}")]
		public virtual async Task<IActionResult> Select(int id)
		{
			IActionResult output;

			try
			{
				output = Ok(_appService.Select(id));
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}

		[HttpGet]
		public virtual async Task<IActionResult> Select()
		{
			IActionResult output;

			try
			{
				output = Ok(_appService.Select());
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}

		[HttpPut("{id}")]
		public virtual async Task<IActionResult> Update(int id, TEntityViewModel viewModel)
		{
			IActionResult output;

			try
			{
				output = Ok(_appService.Update(id, viewModel));
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}
	}
}
