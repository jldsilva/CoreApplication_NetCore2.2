using Core.Data.Context;
using Core.Domain.Entities;
using Core.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace Core.Data.Repositories
{
	public class UserRepository : BaseRepository<MainDbContext, User>, IUserRepository
	{
		private readonly MainDbContext _dbContext;

		public UserRepository(MainDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public User GetByEmail(string email)
		{
			return base.Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public User GetByUserName(string usrname)
		{
			return base.Where(x => x.UserName.Equals(usrname)).FirstOrDefault();
		}

		public override User Update(User entity)
		{
			entity.LastUpdate = DateTime.Now;
			return base.Update(entity);
		}
	}
}
