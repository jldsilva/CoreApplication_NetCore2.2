using Core.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Core.Api
{
	public partial class Startup
	{
		private void InitializeContainer(IApplicationBuilder app)
		{
			app.UseSimpleInjector(container);
			BootStrapper.RegisterServices(container);
		}

		private void SetServiceSimpleInjector(IServiceCollection services)
		{
			container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			services.AddSimpleInjector(container, options =>
			{
				options.AddAspNetCore()
						 .AddControllerActivation()
						 .AddViewComponentActivation()
						 .AddPageModelActivation()
						 .AddTagHelperActivation();
			});
		}
	}
}
