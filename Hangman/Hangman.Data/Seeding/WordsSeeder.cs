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
            if(context.Words.Count() > 0)
            {
                return;
            }

            var ids = context.Categories.ToDictionary(x => x.Name, y => y.Id);
            string technologiesKey = "Technologies";
            string sportKey = "Sport";
            string animalsKey = "Animals";
            var words = new List<Word>()
            {
                new Word { Content = "blazor", WordDifficulty = WordDifficulty.Easy, CategoryId = ids[technologiesKey] },
                new Word { Content = "webapi", WordDifficulty = WordDifficulty.Medium, CategoryId = ids[technologiesKey] },
                new Word { Content = "programming", WordDifficulty = WordDifficulty.Hard, CategoryId = ids[technologiesKey] },
                new Word { Content = "dog", WordDifficulty = WordDifficulty.Easy, CategoryId = ids[animalsKey] },
                new Word { Content = "tiger", WordDifficulty = WordDifficulty.Medium, CategoryId = ids[animalsKey] },
                new Word { Content = "elephant", WordDifficulty = WordDifficulty.Hard, CategoryId = ids[animalsKey] },
                new Word { Content = "football", WordDifficulty = WordDifficulty.Medium, CategoryId = ids[sportKey] },
                new Word { Content = "tennis", WordDifficulty = WordDifficulty.Easy, CategoryId = ids[sportKey] },
                new Word { Content = "basketball", WordDifficulty = WordDifficulty.Hard, CategoryId = ids[sportKey] },
                new Word { Content = "Bossaball", WordDifficulty = WordDifficulty.Hard, CategoryId = ids[sportKey] }
            };

            foreach (var word in words)
            {
                word.Content = word.Content.ToUpper();
                await context.Words.AddAsync(word);
            }

            await context.SaveChangesAsync();
		}
	}
}
