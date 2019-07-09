using Hangman.Common;
using System;
using System.Collections.Generic;

namespace Hangman.Data
{
	public class InMemoryWordRepository : IWordRepository
	{
		private const string ExistingWordError = "The given word already exists.";
		private const string NoWordsError = "There are no words.";
		private readonly List<string> words;

		public InMemoryWordRepository()
		{
			words = new List<string>();
		}

		public void AddWord(string word)
		{
			if(this.words.Contains(word))
			{
				throw new ArgumentException(ExistingWordError);
			}

			words.Add(word);
		}

		public string GetRandomWord()
		{
			if(words.Count == 0)
			{
				throw new ArgumentException(NoWordsError);
			}

			Random random = new Random();
			int randomIndex = random.Next(words.Count - 1);
			return words[randomIndex];
		}
	}
}
