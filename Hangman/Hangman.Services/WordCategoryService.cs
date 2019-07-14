using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangman.Data;
using Hangman.Mappings;
using Hangman.Models;
using Hangman.Shared.InputModels.WordCategory;
using Hangman.Shared.ResponseModels.WordCategory;

namespace Hangman.Services
{
    public class WordCategoryService : IWordCategoryService
    {
        private readonly ApplicationDbContext context;

        public WordCategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<WordCategoryResponseModel> All()
        {
            return this.context.Categories.To<WordCategoryResponseModel>().ToList();
        }

        public async Task<WordCategory> Create(WordCategoryCreateInputModel model)
        {
            var wordCategory = model.To<WordCategory>();
            await context.Categories.AddAsync(wordCategory);
            await context.SaveChangesAsync();
            return wordCategory;
        }

        public string GetNameById(int id)
        {
            return this.context.Categories.FirstOrDefault(x => x.Id == id)?.Name;
        }
    }
}
