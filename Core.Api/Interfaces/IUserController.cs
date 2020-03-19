using Core.Api.TokenProvider;
using Core.Application.ViewModels;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Api.Interfaces
{
	public interface IUserController : IBaseApiController<User, UserViewModel>
	{
		Task<IActionResult> GetByUserName(string usrname);
		Task<IActionResult> GetByEmail(string email);
		Task<IActionResult> Login([FromBody]UserViewModel user, [FromServices]SigningConfiguration signingConfiguration, [FromServices]TokenConfiguration tokenConfiguration);
	}
}
