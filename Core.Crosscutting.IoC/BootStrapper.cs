using Core.Application.AppServices;
using Core.Application.Interfaces;
using Core.Data.Repositories;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Interfaces.Services;
using Core.Domain.Services;
using SimpleInjector;

namespace Core.CrossCutting.IoC
{
	public class BootStrapper
	{
		public static void RegisterServices(Container container)
		{
			#region [Application]
			//container.Register<IEntityAppService, EntityAppService>(Lifestyle.Scoped);
			container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);
			#endregion

			#region [Services]
			//container.Register<IEntityService, EntityService>(Lifestyle.Scoped);
			container.Register<IUserService, UserService>(Lifestyle.Scoped);
			#endregion

			#region [Repositories]
			//container.Register<IEntityRepository, EntityRepository>(Lifestyle.Scoped);
			container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
			#endregion
		}
	}
}
