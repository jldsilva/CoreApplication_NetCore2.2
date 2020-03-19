using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.ViewModels;
using Core.Domain.Entities;
using Core.Domain.Interfaces.Services;

namespace Core.Application.AppServices
{
	public class UserAppService : BaseAppService<User, UserViewModel>, IUserAppService
	{
		private readonly IUserService _service;

		public UserAppService(IUserService service) : base(service)
		{
			_service = service;
		}

		public UserViewModel GetByEmail(string email)
		{
			var entity = _service.GetByEmail(email);

			return Mapper.Map<User, UserViewModel>(entity);
		}

		public UserViewModel GetByUserName(string usrname)
		{
			var entity = _service.GetByUserName(usrname);

			return Mapper.Map<User, UserViewModel>(entity);
		}
	}
}
