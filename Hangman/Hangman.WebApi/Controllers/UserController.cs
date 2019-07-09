using Hangman.Common;
using Hangman.Models;
using Hangman.Services;
using Hangman.Shared.InputModels.User;
using Hangman.WebApi.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Hangman.WebApi.Controllers
{
	public class UserController : ApiController
	{
		private readonly IUserService userService;
		private readonly TokenProvider tokenProvider;

		public UserController(IUserService userService, TokenProvider tokenProvider)
		{
			this.userService = userService;
			this.tokenProvider = tokenProvider;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult> Register(UserRegisterInputModel model)
		{
			bool userWithTheSameUsernameOrEmailExists = userService.UserWithTheSameUsernameOrEmailExists(model.Username, model.Email);
			if(userWithTheSameUsernameOrEmailExists)
			{
				return BadRequest(ErrorMessages.UserWithTheSameUsernameOrEmailExists);
			}

			ApplicationUser user = await userService.CreateUser(model);
			return Ok();
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<string>> Login(UserLoginInputModel model, [FromServices]IOptions<AuthenticationSettings> settings)
		{
			ApplicationUser user = await userService.GetUserByUserNameAndPassword(model.Username, model.Password);
			if(user == null)
			{
				return BadRequest(ErrorMessages.InvalidUserNamrOrPassword);
			}

			var token = tokenProvider.GenerateToken(user.Username, user.Id, settings.Value.Secret);
			return token;
		}
	}
}
