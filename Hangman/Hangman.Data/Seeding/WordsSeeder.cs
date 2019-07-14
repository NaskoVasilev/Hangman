using Hangman.Models;
using Hangman.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
	public class WordsSeeder : ISeeder
	{
		public async Task Seed(IServiceProvider serviceProvider)
		{
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if(context.Categories.Count() > 0)
            {
                return;
            }

            var words = new List<Word>()
            {
                new Word { Content = "blazor", WordDifficulty = WordDifficulty.Easy, CategoryId = 3 },
                new Word { Content = "webapi", WordDifficulty = WordDifficulty.Medium, CategoryId = 3 },
                new Word { Content = "programming", WordDifficulty = WordDifficulty.Hard, CategoryId = 3 },
                new Word { Content = "dog", WordDifficulty = WordDifficulty.Easy, CategoryId = 1 },
                new Word { Content = "tiger", WordDifficulty = WordDifficulty.Medium, CategoryId = 1 },
                new Word { Content = "elephant", WordDifficulty = WordDifficulty.Hard, CategoryId = 1 },
                new Word { Content = "football", WordDifficulty = WordDifficulty.Medium, CategoryId = 2 },
                new Word { Content = "tennis", WordDifficulty = WordDifficulty.Easy, CategoryId = 3 },
                new Word { Content = "basketball", WordDifficulty = WordDifficulty.Hard, CategoryId = 4 },
                new Word { Content = "Bossaball", WordDifficulty = WordDifficulty.Hard, CategoryId = 4 }
            };

            foreach (var word in words)
            {
                await context.Words.AddAsync(word);
            }

            await context.SaveChangesAsync();
		}
	}
}
