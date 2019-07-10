﻿using Hangman.Common;
using Hangman.Services;
using Hangman.WebApi.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.WebApi.Infrastrucure.Middlewares
{
	public class AuthenticationMiddleware
	{
		private const string AuthorizationHeaderKey = "Authorization";
		private readonly RequestDelegate next;

		public AuthenticationMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext context, IUserService userService, IHasher hasher, 
			IOptions<AuthenticationSettings> settings, UserPrincipal userPrincipal)
		{
			var authorizationHeaderExists = context.Request.Headers.TryGetValue(AuthorizationHeaderKey, out StringValues value);

			if (authorizationHeaderExists)
			{
				//Format: Bearer tokenValue
				string token = value.First().Split(' ')[1];
				string[] pairs = token.Split('.');
				
				var userData = JsonConvert.DeserializeObject<UserData>(pairs[0]);

				//Validate token
				if(hasher.Hash(userData.UserId + settings.Value.Secret) == pairs[1])
				{
					userPrincipal.UserId = userData.UserId;
					userPrincipal.Username = userData.Username;

					//var identityType = context.User.Identity.GetType()
					//var field = identityType.GetField("<Name>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
					//	.SetValue(context.User.Identity, userData.Username);
					//identityType.GetField("<IsAuthenticated>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
					//	.SetValue(context.User.Identity, userData.Username);
				}
			}

			await next(context);
		}
	}

}