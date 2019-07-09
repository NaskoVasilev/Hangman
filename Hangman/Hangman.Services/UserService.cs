using Hangman.Data;
using Hangman.Models;

namespace Hangman.Services
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext context;

		public UserService(ApplicationDbContext context)
		{
			this.context = context;
		}
	}
}
