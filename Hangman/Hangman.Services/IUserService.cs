using Hangman.Models;
using Hangman.Shared.InputModels.User;
using System.Threading.Tasks;

namespace Hangman.Services
{
	public interface IUserService
	{
		Task<ApplicationUser> CreateUser(UserRegisterInputModel model);

		bool UserWithTheSameUsernameOrEmailExists(string username, string email);

		Task<ApplicationUser> GetUserByUserNameAndPassword(string username, string password);

		ApplicationUser GetById(string userId);

		bool IsInRole(string role, string userId);
	}
}
