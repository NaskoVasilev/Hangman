using Hangman.Common;

namespace Hangman.Data.Seeding
{
	public class WordsSeeder : ISeeder
	{
		public void Seed(IWordRepository repository)
		{
			string[] words = new string[]
			{
				"mission",
				"monkey",
				"selection",
				"blazor",
				"webapi",
				"browser",
				"csharp",
				"development",
				"technology",
				"claim",
				"habit",
				"football"
			};

			foreach (var word in words)
			{
				repository.AddWord(word);
			}
		}
	}
}
