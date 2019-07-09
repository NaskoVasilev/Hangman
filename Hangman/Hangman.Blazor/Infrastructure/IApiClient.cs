using System.Threading.Tasks;

namespace Hangman.Blazor.Infrastructure
{
	public interface IApiClient
	{
		Task<string> GetRandomWord();
	}
}
