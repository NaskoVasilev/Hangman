using Hangman.Models.Enums;

namespace Hangman.Services
{
	public interface IWordService
	{
		string GetRandomWord(WordDifficulty wordDifficulty, int categoryId); 
	}
}
