using Hangman.Data;
using Hangman.Data.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Hangman.WebApi.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static void UseDataSeeders(this IApplicationBuilder applicationBuilder)
		{
			List<ISeeder> seeders = new List<ISeeder>()
			{
				new RolesSeeder(),
				new AdminUserSeeder(),
				new WordsSeeder()
			};

			using(var scope = applicationBuilder.ApplicationServices.CreateScope())
			{
				ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				foreach (var seeder in seeders)
				{
					seeder.Seed(context).GetAwaiter().GetResult();
				}
			}
		}
	}
}
