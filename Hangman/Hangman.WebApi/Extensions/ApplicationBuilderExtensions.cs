using Hangman.Data;
using Hangman.Data.Seeding;
using Hangman.WebApi.Infrastrucure.Middlewares;
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
				new CategoriesSeeder(),
                new WordsSeeder()
			};

			using(var scope = applicationBuilder.ApplicationServices.CreateScope())
			{
				foreach (var seeder in seeders)
				{
					seeder.Seed(scope.ServiceProvider).GetAwaiter().GetResult();
				}
			}
		}

		public static void UseTokenBasedAuthentication(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseMiddleware<AuthenticationMiddleware>();
		}
	}
}
