using Hangman.Common;
using Hangman.Models;
using Hangman.Services;
using Hangman.Shared.InputModels.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hangman.WebApi.Controllers
{
	public class UserController : ApiController
	{
		private readonly IUserService userService;

		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<ApplicationUser>> Register(UserRegisterInputModel model)
		{
			bool userWithTheSameUsernameOrEmailExists = userService.UserWithTheSameUsernameOrEmailExists(model.Username, model.Email);
			if(userWithTheSameUsernameOrEmailExists)
			{
				return BadRequest(ErrorMessages.UserWithTheSameUsernameOrEmailExists);
			}

			ApplicationUser user = await userService.CreateUser(model);
			return user;
		}

		public async Task<ActionResult<string>> Login(UserLoginInputModel model)
		{
			return null;
		}
	}
}
