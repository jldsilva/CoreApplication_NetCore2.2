using Core.Domain.Entities;

namespace Core.Domain.Interfaces.Services
{
	public interface IUserService : IBaseService<User>
	{
		User GetByEmail(string email);
		User GetByUserName(string usrname);
	}
}
