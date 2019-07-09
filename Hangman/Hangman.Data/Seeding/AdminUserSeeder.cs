using Hangman.Models;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
	public class AdminUserSeeder : ISeeder
	{
		public async Task Seed(ApplicationDbContext context)
		{
			ApplicationUser admin = new ApplicationUser()
			{
				Username = "admin",
				Email = "adimn@admin.com",
				Password = "123456"
			};

			await context.AddAsync(admin);
			await context.SaveChangesAsync();
		}
	}
}
