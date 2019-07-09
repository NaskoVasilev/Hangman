namespace Hangman.Services
{
	public interface IHasher
	{
		string Hash(string content);
	}
}
