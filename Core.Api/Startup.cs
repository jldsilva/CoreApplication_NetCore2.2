using Core.Application.Helpers;
using Core.Application.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using System.Net;

namespace Core.Api
{
	public partial class Startup
	{
		protected Container container = new Container();

		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
		{
			if (!env.IsDevelopment())
				IIS_DirectoryHelper.SetCurrentDirectory();

			var builder = new ConfigurationBuilder()
					 .SetBasePath(env.ContentRootPath)
					 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
					 .AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => {
						builder.WithOrigins("http://localhost:4200")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials();
					}
				);
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("AllUsers", policy => policy.RequireAuthenticatedUser());
			});

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
			services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(container));
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.EnableSimpleInjectorCrossWiring(container);
			services.UseSimpleInjectorAspNetRequestScoping(container);
			services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			SetServiceAuth(services);
			SetServiceDbContext(services);
			SetServiceSimpleInjector(services);
			SetServiceSwagger(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseExceptionHandler(
				options =>
				{
					options.Run(
						async context =>
						{
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
							context.Response.ContentType = "text/html";
							var ex = context.Features.Get<IExceptionHandlerFeature>();
							if (ex != null)
							{
								var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
								await context.Response.WriteAsync(err).ConfigureAwait(false);
							}
						}
					);
				}
			);

			app.UseCors("CorsPolicy");
			app.UseAuthentication();
			app.UseCookiePolicy();
			app.UseStaticFiles();
			app.UseMvc();

			ConfigureSwagger(app);
			InitializeContainer(app);
			container.Verify();
			AutoMapperConfig.RegisterMappings();
		}
	}
}
