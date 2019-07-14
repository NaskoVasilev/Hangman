using Hangman.Shared;
using Hangman.Shared.InputModels.WordCategory;
using Hangman.Shared.ResponseModels.WordCategory;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class WordCategoryCreateComponent : BaseHangmanComponent
    {
        public WordCategoryCreateComponent()
        {
            this.Model = new WordCategoryCreateInputModel();
        }

        public ApiResponse<WordCategoryResponseModel> Response { get; set; }

        public WordCategoryCreateInputModel Model { get; set; }

        public async Task CreateCategory()
        {
            Response= await ApiClient.CreateCategory(Model);
            if(Response.IsOk)
            {
                UriHelper.NavigateTo("/");
            }
        }
    }
}
