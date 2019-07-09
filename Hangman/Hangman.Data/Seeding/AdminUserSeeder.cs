using Hangman.Common;
using Hangman.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
	public class AdminUserSeeder : ISeeder
	{
		public async Task Seed(ApplicationDbContext context)
		{
			string username = "admin";
			if (!context.Users.Any(u => u.Username == username))
			{
				ApplicationRole role = context.Roles.FirstOrDefault(r => r.Name == GlobalConstants.AdministratorRole);
				if (role != null)
				{
					ApplicationUser admin = new ApplicationUser()
					{
						Username = username,
						Email = "adimn@admin.com",
						Password = "123456",
					};

					await context.AddAsync(admin);
					await context.UserRoles.AddAsync(new ApplicationUserRole { Role = role, User = admin });
					await context.SaveChangesAsync();
				}
			}
		}
	}
}
