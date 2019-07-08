namespace Hangman.Common
{
	public interface IWordRepository
	{
		void AddWord(string word);

		string GetRandomWord();
	}
}
