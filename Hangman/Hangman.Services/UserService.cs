using System.Linq;
using System.Threading.Tasks;
using Hangman.Data;
using Hangman.Mappings;
using Hangman.Models;
using Hangman.Shared.InputModels.User;

namespace Hangman.Services
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext context;
		private readonly IHasher hasher;

		public UserService(ApplicationDbContext context, IHasher hasher)
		{
			this.context = context;
			this.hasher = hasher;
		}

		public async Task<ApplicationUser> CreateUser(UserRegisterInputModel model)
		{
			ApplicationUser user = model.To<ApplicationUser>();
			user.Password = hasher.Hash(user.Password);
			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();
			return user;
		}

		public bool UserWithTheSameUsernameOrEmailExists(string username, string email)
		{
			return context.Users.Any(u => u.Username == username || u.Email == email);
		}
	}
}
