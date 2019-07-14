﻿using Hangman.Models.Enums;
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

        //TODO: Authoraize this action
        [HttpGet("[action]")]
        public ActionResult<ApiResponse<string>> GetRandomWord(int categoryId, WordDifficulty level = WordDifficulty.Easy)
		{
            return new ApiResponse<string>(wordService.GetRandomWord(level, categoryId));
		}
	}
}
