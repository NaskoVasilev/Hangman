using Hangman.Services;
using Hangman.Shared;
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
		public ActionResult<ApiResponse<string>> GetRandomWord()
		{
			return new ApiResponse<string>(wordService.GetRandomWord());
		}
	}
}
