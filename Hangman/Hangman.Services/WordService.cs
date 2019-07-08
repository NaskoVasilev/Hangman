using Hangman.Common;

namespace Hangman.Services
{
	public class WordService : IWordService
	{
		private readonly IWordRepository wordRepository;

		public WordService(IWordRepository wordRepository)
		{
			this.wordRepository = wordRepository;
		}

		public string GetRandomWord()
		{
			return wordRepository.GetRandomWord();
		}
	}
}
