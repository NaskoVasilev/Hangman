using Hangman.Shared;
using Hangman.Shared.InputModels.Word;
using Hangman.Shared.InputModels.WordCategory;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class EditWordComponent : BaseHangmanComponent
    {
        public EditWordComponent()
        {
            this.Model = new WordEditInputModel();
            this.WordCategories = new List<WordCategoryViewModel>();
            this.WordDifficulties = new List<string>();
        }

        [Parameter]
        public string Id { get; set; }

        public WordEditInputModel Model { get; set; }

        public ApiResponse<WordEditResponseModel> GetDataResponse { get; set; }

        public IEnumerable<string> WordDifficulties { get; set; }

        public IEnumerable<WordCategoryViewModel> WordCategories { get; set; }

        public ApiResponse<bool> SendDataResponse { get; set; }

        protected override async Task OnInitAsync()
        {
            this.GetDataResponse = await ApiClient.GetWordForEditing(int.Parse(this.Id));
            if(this.GetDataResponse.IsOk)
            {
                this.WordDifficulties = GetDataResponse.Data.WordDifficultes;
                this.WordCategories = GetDataResponse.Data.WordCategories;
                Model.Content = GetDataResponse.Data.Content;
                Model.CategoryId = GetDataResponse.Data.CategoryId;
                Model.WordDifficulty = GetDataResponse.Data.WordDifficulty;
            }
            else
            {
                this.GetDataResponse.AddError("Invalid id was provided");
            }
        }

        public async Task EditHandler()
        {
            Model.Id = int.Parse(this.Id);
            this.SendDataResponse = await ApiClient.EditWord(Model);
            if(SendDataResponse.IsOk)
            {
                UriHelper.NavigateTo("/word/all");
            }
        }
    }
}
