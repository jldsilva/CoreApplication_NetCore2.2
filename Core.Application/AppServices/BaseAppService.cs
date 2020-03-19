using AutoMapper;
using Core.Application.Interfaces;
using Core.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Core.Application.AppServices
{
	public class BaseAppService<TEntity, TEntityViewModel> : IDisposable, IBaseAppService<TEntity, TEntityViewModel> where TEntity : class where TEntityViewModel : class
	{
		private readonly IBaseService<TEntity> _baseService;
		private bool disposed = false;

		public BaseAppService(IBaseService<TEntity> baseService)
		{
			_baseService = baseService;
		}

		public virtual TEntityViewModel Add(TEntityViewModel viewModel)
		{
			if (viewModel == null) throw new InvalidOperationException();

			var model = _baseService.Add(Mapper.Map<TEntityViewModel, TEntity>(viewModel));

			return Mapper.Map<TEntity, TEntityViewModel>(model);
		}

		public virtual void Remove(int id)
		{
			_baseService.Remove(id);
		}

		public virtual void Remove(TEntityViewModel viewModel)
		{
			if (viewModel == null) throw new InvalidOperationException();

			_baseService.Remove(Mapper.Map<TEntityViewModel, TEntity>(viewModel));
		}

		public virtual TEntityViewModel Select(int id)
		{
			var model = _baseService.Select(id);

			return Mapper.Map<TEntity, TEntityViewModel>(model);
		}

		public virtual IEnumerable<TEntityViewModel> Select()
		{
			var list = _baseService.Select();

			return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityViewModel>>(list);
		}

		public virtual TEntityViewModel Update(TEntityViewModel viewModel)
		{
			if (viewModel == null) throw new InvalidOperationException();

			var model = _baseService.Update(Mapper.Map<TEntityViewModel, TEntity>(viewModel));

			return Mapper.Map<TEntity, TEntityViewModel>(model);

		}

		public virtual TEntityViewModel Update(int id, TEntityViewModel viewModel)
		{
			if (viewModel == null) throw new InvalidOperationException();

			var model = Mapper.Map<TEntityViewModel, TEntity>(viewModel);
			var property = model.GetType().GetProperty(string.Concat(typeof(TEntity).Name, "Id"));
			property.SetValue(model, id, new object[0]);

			return Mapper.Map<TEntity, TEntityViewModel>(_baseService.Update(model));
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_baseService.Dispose();
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
