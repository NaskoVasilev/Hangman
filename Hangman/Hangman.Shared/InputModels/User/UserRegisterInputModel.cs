using System.ComponentModel.DataAnnotations;

namespace Hangman.Shared.InputModels.User
{
	public class UserRegisterInputModel
	{
		private const int UsernameMinLength = 5;
		private const int PasswordMinLength = 5;

		[Required]
		[MinLength(UsernameMinLength)]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(PasswordMinLength)]
		[Compare(nameof(ConfirmPassword)]
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }
	}
}
