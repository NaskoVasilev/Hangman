using Hangman.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.WebApi.Controllers
{
	public class WordController : ApiController
	{
		private readonly IWordService wordService;

		public WordController(IWordService wordService)
		{
			this.wordService = wordService;
		}

		[HttpGet("[action]")]
		public ActionResult<string> GetRandomWord()
		{
			return wordService.GetRandomWord();
		}
	}
}
