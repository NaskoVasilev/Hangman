using Hangman.Common;
using Hangman.Models;
using Hangman.Services;
using Hangman.Shared;
using Hangman.Shared.InputModels.User;
using Hangman.Shared.ResponseModels;
using Hangman.WebApi.Authentication;
using Hangman.WebApi.Infrastrucure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Hangman.Mappings;

namespace Hangman.WebApi.Controllers
{
	public class UserController : ApiController
	{
		private readonly IUserService userService;
		private readonly TokenProvider tokenProvider;
		private readonly UserPrincipal userPrincipal;

		public UserController(IUserService userService, TokenProvider tokenProvider, UserPrincipal userPrincipal)
		{
			this.userService = userService;
			this.tokenProvider = tokenProvider;
			this.userPrincipal = userPrincipal;
		}

		[HttpPost("[action]")]
		public async Task<ApiResponse<UserResponseModel>> Register(UserRegisterInputModel model)
		{
            if (!ModelState.IsValid)
            {
                return this.ModelStateErrors<UserResponseModel>();
            }

			bool userWithTheSameUsernameOrEmailExists = userService.UserWithTheSameUsernameOrEmailExists(model.Username, model.Email);
			if(userWithTheSameUsernameOrEmailExists)
			{
                return this.Error<UserResponseModel>(ErrorMessages.UserWithTheSameUsernameOrEmailExists);
			}

			ApplicationUser user = await userService.CreateUser(model);
            return new ApiResponse<UserResponseModel>(user.To<UserResponseModel>());
		}

		[HttpPost("[action]")]
		public async Task<ApiResponse<string>> Login(UserLoginInputModel model, [FromServices]IOptions<AuthenticationSettings> settings)
		{
			ApplicationUser user = await userService.GetUserByUserNameAndPassword(model.Username, model.Password);
			if(user == null)
			{
                return this.Error<string>(ErrorMessages.InvalidUserNamrOrPassword);
			}

			var token = tokenProvider.GenerateToken(user.Username, user.Id, settings.Value.Secret);
			return new ApiResponse<string>(token);
		}

		[Authorization]
		[HttpGet("me")]
		public ActionResult<string> GetUsername()
		{
			return userPrincipal.Username;
		}

		[Authorization(Roles = "Admin")]
		[HttpGet("admin")]
		public ActionResult<string> GetAdminData()
		{
			return userPrincipal.UserId;
		}
	}
}
