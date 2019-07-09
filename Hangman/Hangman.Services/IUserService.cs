using Hangman.Models;
using Hangman.Shared.InputModels.User;
using System.Threading.Tasks;

namespace Hangman.Services
{
	public interface IUserService
	{
		Task<ApplicationUser> CreateUser(UserRegisterInputModel model);

		bool UserWithTheSameUsernameOrEmailExists(string username, string email);
	}
}
