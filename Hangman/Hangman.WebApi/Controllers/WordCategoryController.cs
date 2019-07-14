using Hangman.Services;
using Hangman.Shared;
using Hangman.Shared.InputModels.WordCategory;
using Hangman.Shared.ResponseModels.WordCategory;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.WebApi.Controllers
{
    public class WordCategoryController : ApiController
    {
        private readonly IWordCategoryService wordCategoryService;

        public WordCategoryController(IWordCategoryService wordCategoryService)
        {
            this.wordCategoryService = wordCategoryService;
        }

        [Route("[action]")]
        public async Task<ApiResponse<ActionResult>> Create(WordCategoryCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelStateErrors<ActionResult>();
            }

            await wordCategoryService.Create(model);
            return new ApiResponse<ActionResult>(this.Ok());
        }

        public ApiResponse<IEnumerable<WordCategoryResponseModel>> All()
        {
            IEnumerable<WordCategoryResponseModel> categories = wordCategoryService.All();
            return new ApiResponse<IEnumerable<WordCategoryResponseModel>>(categories);
        }
    }
}
