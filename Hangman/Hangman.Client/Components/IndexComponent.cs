using Hangman.Common;
using Hangman.Models.Enums;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class IndexComponent : BaseHangmanComponent
    {
        public WordDifficulty Level { get; set; } = WordDifficulty.Easy;

        public async Task StartGame()
        {
            await JsInterop.SetSessionStorageItem(nameof(WordDifficulty), Level.ToString());
            UriHelper.NavigateTo("/game");
        }

        protected override void OnInit()
        {
            this.ApplicationState.OnUserDataChange += this.StateHasChanged;
            base.OnInit();
        }
    }
}
