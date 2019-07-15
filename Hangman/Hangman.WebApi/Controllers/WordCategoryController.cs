using Hangman.Services;
using Hangman.Shared;
using Hangman.Shared.InputModels.WordCategory;
using Hangman.Shared.ResponseModels.WordCategory;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangman.Mappings;
using Hangman.WebApi.Infrastrucure.Attributes;
using Hangman.Common;

namespace Hangman.WebApi.Controllers
{
    public class WordCategoryController : ApiController
    {
        private readonly IWordCategoryService wordCategoryService;

        public WordCategoryController(IWordCategoryService wordCategoryService)
        {
            this.wordCategoryService = wordCategoryService;
        }

        [Authorization(Roles = GlobalConstants.AdministratorRole)]
        [Route("[action]")]
        public async Task<ApiResponse<WordCategoryResponseModel>> Create(WordCategoryCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelStateErrors<WordCategoryResponseModel>();
            }

            var wordCategory = await wordCategoryService.Create(model);
            return new ApiResponse<WordCategoryResponseModel>(wordCategory.To<WordCategoryResponseModel>());
        }

        [Authorization]
        [Route("[action]")]
        public ApiResponse<IEnumerable<WordCategoryResponseModel>> All()
        {
            IEnumerable<WordCategoryResponseModel> categories = wordCategoryService.All();
            return new ApiResponse<IEnumerable<WordCategoryResponseModel>>(categories);
        }

        [Authorization]
        [Route("[action]")]
        public ApiResponse<string> GetName(int id)
        {
            string name = wordCategoryService.GetNameById(id);
            return new ApiResponse<string>(name);
        }
    }
}
