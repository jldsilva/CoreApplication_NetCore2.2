using Core.Domain.Entities;

namespace Core.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
		User GetByEmail(string email);
		User GetByUserName(string usrname);
	}
}
