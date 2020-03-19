using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Core.Api.TokenProvider
{
	public static class TokenProviderMiddleware
	{
		public static object GenerateToken(string name, bool isValid, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration)
		{
			object result;

			if (isValid)
			{
				var identity = new ClaimsIdentity(
					new GenericIdentity(name, "Login"),
					new[] {
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
							new Claim(JwtRegisteredClaimNames.UniqueName, name)
					}
				);

				DateTime dataCriacao = DateTime.Now;
				DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfiguration.Seconds);

				var handler = new JwtSecurityTokenHandler();
				var securityToken = handler.CreateToken(new SecurityTokenDescriptor
				{
					Issuer = tokenConfiguration.Issuer,
					Audience = tokenConfiguration.Audience,
					SigningCredentials = signingConfiguration.SigningCredentials,
					Subject = identity,
					NotBefore = dataCriacao,
					Expires = dataExpiracao
				});
				var token = handler.WriteToken(securityToken);

				result = new
				{
					accessToken = token,
					authenticated = true,
					created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
					expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
					message = "OK"
				};
			}
			else
			{
				result = new
				{
					authenticated = false,
					message = "Falha ao autenticar"
				};
			}

			return result;
		}
	}
}
