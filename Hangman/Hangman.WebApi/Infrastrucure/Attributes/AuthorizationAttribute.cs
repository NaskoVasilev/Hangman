using System;

namespace Hangman.WebApi.Infrastrucure.Filters
{
	public class AuthorizationAttribute : Attribute
	{
		public AuthorizationAttribute()
		{
		}

		public AuthorizationAttribute(string roles)
		{
			this.Roles = roles;
		}

		public string Roles { get; set; }
	}
}
