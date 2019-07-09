using Hangman.Data;
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

		public string GetRandomWord()
		{
			if(!context.Words.Any())
			{
				throw new ArgumentException(NoWordsErrorMessage);
			}

			int skippedWordsCount = new Random().Next(context.Words.Count() - 1);
			string content = context.Words
				.Skip(skippedWordsCount)
				.First()
				.Content;
			return content;
		}
	}
}
