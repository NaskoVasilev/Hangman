using Hangman.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
    public class CategoriesSeeder : ISeeder
    {
        public async Task Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if(context.Categories.Count() > 0)
            {
                return;
            }

            List<string> categories = new List<string>()
            {
                "animals",
                "sport",
                "technologies",
            };

            foreach (var category in categories)
            {
                await context.Categories.AddAsync(new WordCategory { Name = category });
            }
            await context.SaveChangesAsync();
        }
    }
}
