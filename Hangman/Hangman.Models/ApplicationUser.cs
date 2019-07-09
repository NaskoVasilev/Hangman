using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
	public class ApplicationUser : BaseModel<string>
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
