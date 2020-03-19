using Core.Api.Interfaces;
using Core.Api.TokenProvider;
using Core.Application.Helpers;
using Core.Application.Interfaces;
using Core.Application.ViewModels;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
	[Route("api/[controller]")]
	public class UserController : BaseApiController<User, UserViewModel>, IUserController
	{
		private readonly IUserAppService _appService;

		public UserController(IUserAppService appService) : base(appService)
		{
			_appService = appService;
		}

		[HttpGet("getByEmail/{email}")]
		public async Task<IActionResult> GetByEmail(string email)
		{
			IActionResult output;

			try
			{
				output = Ok(_appService.GetByEmail(email));
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}

		[Authorize("Bearer")]
		[HttpGet("getByUserName/{usrname}")]
		public async Task<IActionResult> GetByUserName(string usrname)
		{
			IActionResult output;

			try
			{
				output = Ok(_appService.GetByUserName(usrname));
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody]UserViewModel user, [FromServices]SigningConfiguration signingConfiguration, [FromServices]TokenConfiguration tokenConfiguration)
		{
			IActionResult output;

			try
			{
				var _user = _appService.GetByUserName(user.UserName);
				var isValid = (_user.UserName == user.UserName && _user.AccessKey.Equals(CryptoHelper.GenerateSHA256String(user.AccessKey)));
				output = Ok(TokenProviderMiddleware.GenerateToken(user.UserName, isValid, signingConfiguration, tokenConfiguration));
			}
			catch (Exception ex)
			{
				output = BadRequest(ex?.Message);
			}

			return await Task.FromResult(output);
		}
	}
}
