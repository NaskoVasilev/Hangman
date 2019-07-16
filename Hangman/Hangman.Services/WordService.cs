using Hangman.Data;
using Hangman.Mappings;
using Hangman.Models;
using Hangman.Models.Enums;
using Hangman.Shared.InputModels.Word;
using Hangman.Shared.InputModels.WordCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Services
{
	public class WordService : IWordService
	{
		private const string NoWordsErrorMessage = "There are no words in the database.";
		private readonly ApplicationDbContext context;

		public WordService(ApplicationDbContext context)
		{
			this.context = context;
		}

        public async Task<Word> Create(WordCreateInputModel model)
        {
            model.Content = model.Content.ToUpper();
            Word word = model.To<Word>();
            await context.Words.AddAsync(word);
            await context.SaveChangesAsync();
            return word;
        }

        public async Task<Word> Edit(WordEditInputModel model)
        {
            model.Content = model.Content.ToUpper();
            var word = context.Words.FirstOrDefault(x => x.Id == model.Id);
            word.CategoryId = model.CategoryId;
            word.WordDifficulty = model.WordDifficulty;
            word.Content = model.Content;
            await context.SaveChangesAsync();
            return word;
        }

        public IEnumerable<WordResponseModel> GetAllOrderedByCategoryThenByDifficulty()
        {
            return this.context.Words.
                OrderBy(w => w.Category.Name)
                .ThenBy(w => w.WordDifficulty)
                .To<WordResponseModel>()
                .ToList();
        }

        public string GetRandomWord(WordDifficulty wordDifficulty, int categoryId)
		{
            int wordsCount = context.Words.Count(x => x.WordDifficulty == wordDifficulty && x.CategoryId == categoryId);

            if (wordsCount == 0)
			{
				throw new ArgumentException(NoWordsErrorMessage);
			}

			int skippedWordsCount = new Random().Next(wordsCount);
			string content = context.Words
                .Where(w => w.WordDifficulty == wordDifficulty && w.CategoryId == categoryId)
				.Skip(skippedWordsCount)
				.First()
				.Content;
			return content;
		}

        public WordEditResponseModel GetWordWithAllCategories(int id)
        {
            var word = context.Words.FirstOrDefault(x => x.Id == id);
            var wordModel = word.To<WordEditResponseModel>();
            wordModel.WordCategories = context.Categories.To<WordCategoryViewModel>();
            wordModel.WordDifficultes = Enum.GetNames(typeof(WordDifficulty));
            return wordModel;
        }
    }
}
