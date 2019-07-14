using System.Threading.Tasks;
using Hangman.Logic;
using Hangman.Models.Enums;
using Hangman.Shared;
using Microsoft.AspNetCore.Components;

namespace Hangman.Client.Components
{
    public class GameComponent : BaseHangmanComponent
    {
        [Inject]
        public GameEngine GameEngine { get; set; }

        public ApiResponse<string> Response { get; set; }

        public string Letter { get; set; } = "";

        protected override async Task OnInitAsync()
        {
            await LoadNewWord();
        }

        public async Task Check()
        {
            this.GameEngine.AddMatchingLetters(this.Letter);

            if (GameEngine.Tracker.GameOver)
            {
                this.StateHasChanged();
                UriHelper.NavigateTo("/gameOver/" + GameEngine.CurrentWord);
                return;
            }

            this.Letter = "";
            if(GameEngine.PlayingWord == GameEngine.CurrentWord)
            {
                await LoadNewWord();
            }
        }

        private async Task LoadNewWord()
        {
            System.Console.WriteLine("here");
            string currentWord = await GetWordFromDatabase();
            GameEngine.InitializeNewWord(currentWord);
        }

        private async Task<string> GetWordFromDatabase()
        {
            string level = await JsInterop.GetSessionStorageItem(nameof(WordDifficulty));
            this.Response = await this.ApiClient.GetRandomWord(level);
            return this.Response.Data;
        }
    }
}
