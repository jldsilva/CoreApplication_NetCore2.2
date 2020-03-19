using Core.Api.TokenProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Core.Api
{
	public partial class Startup
	{
		private void SetServiceAuth(IServiceCollection services)
		{
			var signingConfiguration = new SigningConfiguration();
			services.AddSingleton(signingConfiguration);

			var tokenConfigurations = new TokenConfiguration();
			new ConfigureFromConfigurationOptions<TokenConfiguration>(
				 Configuration.GetSection("TokenConfiguration")).Configure(tokenConfigurations);
			services.AddSingleton(tokenConfigurations);


			services.AddAuthentication(authOptions =>
			{
				authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(bearerOptions =>
			{
				var paramsValidation = bearerOptions.TokenValidationParameters;
				paramsValidation.IssuerSigningKey = signingConfiguration.Key;
				paramsValidation.ValidAudience = tokenConfigurations.Audience;
				paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

				// Valida a assinatura de um token recebido
				paramsValidation.ValidateIssuerSigningKey = true;

				// Verifica se um token recebido ainda é válido
				paramsValidation.ValidateLifetime = true;

				// Tempo de tolerância para a expiração de um token (utilizado
				// caso haja problemas de sincronismo de horário entre diferentes
				// computadores envolvidos no processo de comunicação)
				paramsValidation.ClockSkew = TimeSpan.Zero;
			});

			// Ativa o uso do token como forma de autorizar o acesso
			// a recursos deste projeto
			services.AddAuthorization(auth =>
			{
				auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
					 .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
					 .RequireAuthenticatedUser().Build());
			});
		}
	}
}
