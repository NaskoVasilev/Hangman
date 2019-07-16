using Hangman.Models;
using Hangman.Models.Enums;
using Hangman.Services;
using Hangman.Shared;
using Hangman.Shared.InputModels.Word;
using Hangman.WebApi.Infrastrucure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hangman.WebApi.Controllers
{
	public class WordController : ApiController
	{
		private readonly IWordService wordService;

		public WordController(IWordService wordService)
		{
			this.wordService = wordService;
		}

        [Authorization]
        [HttpGet("[action]")]
        public ActionResult<ApiResponse<string>> GetRandomWord(int categoryId, WordDifficulty level = WordDifficulty.Easy)
		{
            return new ApiResponse<string>(wordService.GetRandomWord(level, categoryId));
		}

        [Authorization]
        [HttpPost("[action]")]
        public async Task<ApiResponse<bool>> Create(WordCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelStateErrors<bool>();
            }

            await wordService.Create(model);
            return new ApiResponse<bool>(true);
        }

        [Authorization]
        [HttpPost("[action]")]
        public async Task<ApiResponse<bool>> Edit(WordEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelStateErrors<bool>();
            }

            await wordService.Edit(model);
            return new ApiResponse<bool>(true);
        }

        [Authorization]
        [HttpGet("[action]")]
        public ApiResponse<WordEditResponseModel> GetEditModel(int id)
        {
            var model = wordService.GetWordWithAllCategories(id);
            return new ApiResponse<WordEditResponseModel>(model);
        }
    }
}
