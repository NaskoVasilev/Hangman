﻿using Hangman.Common;
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
		public async Task<ApiResponse<AuthenticatedUserResponseModel>> Login(UserLoginInputModel model, [FromServices]IOptions<AuthenticationSettings> settings)
		{
			ApplicationUser user = await userService.GetUserByUserNameAndPassword(model.Username, model.Password);
			if(user == null)
			{
                return this.Error<AuthenticatedUserResponseModel>(ErrorMessages.InvalidUserNamrOrPassword);
			}

			var token = tokenProvider.GenerateToken(user.Username, user.Id, settings.Value.Secret);
            var authenticatedUser = new AuthenticatedUserResponseModel { Username = user.Username, UserToken = token };
			return new ApiResponse<AuthenticatedUserResponseModel>(authenticatedUser);
		}

        [Route("[action]")]
        public ApiResponse<bool> IsAdmin()
        {
            if(string.IsNullOrEmpty(this.userPrincipal.UserId))
            {
                return new ApiResponse<bool>(false);
            }
            bool isAdmin = userService.IsInRole(GlobalConstants.AdministratorRole, this.userPrincipal.UserId);
            return new ApiResponse<bool>(isAdmin);
        }

		[Authorization]
		[HttpGet("me")]
		public ActionResult<ApiResponse<string>> GetUsername()
		{
			return new ApiResponse<string>(this.userPrincipal.Username);
		}

		[Authorization(Roles = "Admin")]
		[HttpGet("admin")]
		public ActionResult<ApiResponse<string>> GetAdminData()
		{
            return new ApiResponse<string>(this.userPrincipal.UserId);
        }
	}
}
