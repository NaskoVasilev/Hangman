using System.Threading.Tasks;

namespace Hangman.Blazor.Logic
{
	public interface IDataAccess
	{
		Task<string> GetRandomWord();
	}
}
