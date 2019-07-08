using Hangman.Common;
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
				new WordsSeeder()
			};

			IWordRepository wordRepository = applicationBuilder.ApplicationServices.GetRequiredService<IWordRepository>();

			foreach (var seeder in seeders)
			{
				seeder.Seed(wordRepository);
			}
		}
	}
}
