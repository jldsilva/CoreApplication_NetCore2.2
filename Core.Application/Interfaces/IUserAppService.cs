using Core.Application.ViewModels;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
	public interface IUserAppService : IBaseAppService<User, UserViewModel>
	{
		UserViewModel GetByEmail(string email);

		UserViewModel GetByUserName(string usrname);
	}
}
