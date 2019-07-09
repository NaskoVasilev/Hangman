namespace Hangman.Data.Seeding
{
	public interface ISeeder
	{
		void Seed(ApplicationDbContext context);
	}
}
