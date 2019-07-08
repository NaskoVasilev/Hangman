using Microsoft.AspNetCore.Blazor.Components;

namespace Hangman.Blazor.Pages
{
	public class WordComponent : BlazorComponent
	{
		public string Character { get; set; }

		public string CurrentWord { get; set; }

		public string StartGame()
		{
		}
	}
}
