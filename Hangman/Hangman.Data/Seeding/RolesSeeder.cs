using Hangman.Common;
using Hangman.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
	public class RolesSeeder : ISeeder
	{
		public async Task Seed(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
			if(!context.Roles.Any())
			{
				await context.Roles.AddAsync(new ApplicationRole { Name = GlobalConstants.PlayerRole });
				await context.Roles.AddAsync(new ApplicationRole { Name = GlobalConstants.AdministratorRole });
				await context.SaveChangesAsync();
			}
		}
	}
}
