using Hangman.Models;
using Hangman.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
	public class WordsSeeder : ISeeder
	{
		public async Task Seed(ApplicationDbContext context)
		{
			Dictionary<WordDifficulty, List<string>> words = new Dictionary<WordDifficulty, List<string>>()
			{
				[WordDifficulty.Easy] = new List<string>() { "claim", "habit", "football", "blazor" },
				[WordDifficulty.Medium] = new List<string>() { "browser", "monkey", "webapi", "csharp" },
				[WordDifficulty.Hard] = new List<string>() { "mission", "selection" },
				[WordDifficulty.Expert] = new List<string>() { "development", "technology" }
			};

			foreach (var kvp in words)
			{
				foreach (var word in kvp.Value)
				{
					await context.AddAsync(new Word { Content = word, WordDifficulty = kvp.Key });
				}
			}

			await context.SaveChangesAsync();
		}
	}
}
