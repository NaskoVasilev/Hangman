using Hangman.Data;
using Hangman.Models.Enums;
using System;
using System.Linq;

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
	}
}
