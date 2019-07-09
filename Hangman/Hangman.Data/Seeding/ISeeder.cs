using System;
using System.Threading.Tasks;

namespace Hangman.Data.Seeding
{
	public interface ISeeder
	{
		Task Seed(IServiceProvider serviceProvider);
	}
}
