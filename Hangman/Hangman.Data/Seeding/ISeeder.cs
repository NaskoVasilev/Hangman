using Hangman.Common;

namespace Hangman.Data.Seeding
{
	public interface ISeeder
	{
		void Seed(IWordRepository repository);
	}
}
