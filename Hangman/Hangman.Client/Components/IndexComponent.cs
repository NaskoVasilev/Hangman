using Hangman.Common;
using Hangman.Models.Enums;
using Hangman.Shared.ResponseModels.WordCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class IndexComponent : BaseHangmanComponent
    {
        public IndexComponent()
        {
            this.WordCategories = new List<WordCategoryResponseModel>();
        }

        public IEnumerable<WordCategoryResponseModel> WordCategories { get; set; }

        public WordDifficulty Level { get; set; } = WordDifficulty.Easy;

        public int CategoryId { get; set; }

        public async Task StartGame()
        {
            await JsInterop.SetSessionStorageItem(nameof(WordDifficulty), Level.ToString());
            await JsInterop.SetSessionStorageItem(GlobalConstants.CategoryIdentifierKey, CategoryId.ToString());
            UriHelper.NavigateTo("/game");
        }

        protected override async Task OnInitAsync()
        {
            this.ApplicationState.OnUserDataChange += this.StateHasChanged;
            if(ApplicationState.IsLoggedIn)
            {
                var response = await ApiClient.GetAllCategories();
                WordCategories = response.Data;
            }
            base.OnInit();
        }
    }
}
