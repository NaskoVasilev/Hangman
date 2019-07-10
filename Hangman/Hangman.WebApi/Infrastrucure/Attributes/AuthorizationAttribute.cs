using System;

namespace Hangman.WebApi.Infrastrucure.Attributes
{
	public class AuthorizationAttribute : Attribute
	{
		public AuthorizationAttribute()
		{
		}

		public AuthorizationAttribute(string roles)
		{
			Roles = roles;
		}

		public string Roles { get; set; }
	}
}
