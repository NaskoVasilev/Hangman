using Hangman.Common;
using Hangman.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class IndexComponent : BaseHangmanComponent
    {
        public IndexComponent()
        {
            this.CategoryId = 1;
        }

        public bool ShouldRenderCategories { get; set; }

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
            if((await JsInterop.GetToken()) != null)
            {
                ShouldRenderCategories = true;
            }
            base.OnInit();
        }
    }
}
