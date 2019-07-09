using Hangman.Common;
using Hangman.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
	public class AdminUserSeeder : ISeeder
	{
		public async Task Seed(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
			var hasher = serviceProvider.GetRequiredService<IHasher>();
			var adminData = serviceProvider.GetRequiredService<IOptions<AdminData>>().Value;

			string username = "admin";
			if (!context.Users.Any(u => u.Username == username))
			{
				ApplicationRole role = context.Roles.FirstOrDefault(r => r.Name == GlobalConstants.AdministratorRole);
				if (role != null)
				{
					ApplicationUser admin = new ApplicationUser()
					{
						Username = adminData.Username,
						Email = adminData.Email,
						Password = hasher.Hash(adminData.Password),
					};

					await context.AddAsync(admin);
					await context.UserRoles.AddAsync(new ApplicationUserRole { Role = role, User = admin });
					await context.SaveChangesAsync();
				}
			}
		}
	}
}
