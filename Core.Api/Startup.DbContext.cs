using Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api
{
	public partial class Startup
	{
		public void SetServiceDbContext(IServiceCollection services)
		{
			services.AddDbContext<MainDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
		}
	}
}
