using Core.Domain.Entities;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Interfaces.Services;

namespace Core.Domain.Services
{
	public class UserService : BaseService<User>, IUserService
	{
		private readonly IUserRepository _repository;

		public UserService(IUserRepository repository) : base(repository)
		{
			_repository = repository;
		}

		public User GetByEmail(string email)
		{
			return _repository.GetByEmail(email);
		}

		public User GetByUserName(string usrname)
		{
			return _repository.GetByUserName(usrname);
		}
	}
}
