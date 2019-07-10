using Hangman.Services;
using Hangman.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Hangman.WebApi.Infrastrucure.Filters
{
	public class AuthorizeFilter : IActionFilter
	{
		private readonly IUserService userService;
		private readonly UserPrincipal user;

		public AuthorizeFilter(IUserService userService, UserPrincipal user)
		{
			this.userService = userService;
			this.user = user;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
			if (controllerActionDescriptor != null)
			{
				var authorizeAttributeObject = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
					.FirstOrDefault(a => a is AuthorizeAttribute);

				if(authorizeAttributeObject != null)
				{
					var authorizeAttribute = authorizeAttributeObject as AuthorizationAttribute;
					if (userService.GetById(user.UserId) == null)
					{
						context.Result = new BadRequestResult();
						return;
					}

					if(!string.IsNullOrEmpty(authorizeAttribute.Roles))
					{
						bool isAtLeastInOneRole = false;
						foreach (var role in authorizeAttribute.Roles.Split(", "))
						{
							bool isInRole = userService.IsInRole(role, user.UserId);
							if(isInRole)
							{
								isAtLeastInOneRole = true;
								return;
							}
						}

						if(!isAtLeastInOneRole)
						{
							context.Result = new BadRequestResult();
							return;
						}
					}
				}
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}
