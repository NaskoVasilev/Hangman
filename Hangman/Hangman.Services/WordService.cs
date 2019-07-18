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
        private readonly ICategoryPredictorService categoryPredictor;
        private readonly IUtilityService utilityService;

        public WordService(ApplicationDbContext context, ICategoryPredictorService categoryPredictor, IUtilityService utilityService)
		{
			this.context = context;
            this.categoryPredictor = categoryPredictor;
            this.utilityService = utilityService;
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

        public async Task UploadWords(string[] words)
        {
            foreach (var content in words)
            {
                if(this.context.Words.Any(w => w.Content.ToUpper() == content.ToUpper()))
                {
                    continue;
                }

                string category = categoryPredictor.PredictCategory(content);
                var categoryFromDb = context.Categories.FirstOrDefault(x => x.Name.ToLower() == category.ToLower());
                if(categoryFromDb == null)
                {
                    categoryFromDb = new WordCategory { Name = utilityService.NormalizeName(category) };
                    await this.context.Categories.AddAsync(categoryFromDb);
                    await this.context.SaveChangesAsync();
                }

                var word = new Word
                {
                    Content = content.ToUpper(),
                    CategoryId = categoryFromDb.Id,
                    WordDifficulty = GetWordDiffuculty(content)
                };

                await context.Words.AddAsync(word);
            }

            await context.SaveChangesAsync();
        }

        private WordDifficulty GetWordDiffuculty(string word)
        {
            if(word.Length < 5)
            {
                return WordDifficulty.Easy;
            }
            else if(word.Length < 7)
            {
                return WordDifficulty.Medium;
            }
            else if(word.Length < 9)
            {
                return WordDifficulty.Hard;
            }
            else
            {
                return WordDifficulty.Expert;
            }
        }
    }
}
