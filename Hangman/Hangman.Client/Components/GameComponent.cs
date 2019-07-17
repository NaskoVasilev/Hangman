using System.Threading.Tasks;
using Hangman.Common;
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

        public string CategoryName { get; set; }

        public string Level { get; set; }

        protected override async Task OnInitAsync()
        {
            this.GameEngine.Tracker.OnStateChange += this.StateHasChanged;
            string categoryId = await JsInterop.GetSessionStorageItem(GlobalConstants.CategoryIdentifierKey);
            var response = await ApiClient.GetCategoryNameById(categoryId);
            this.CategoryName = response.Data;
            this.Level = await JsInterop.GetSessionStorageItem(nameof(WordDifficulty));
            await LoadNewWord();
        }

        public async Task Check(char letter)
        {
            System.Console.WriteLine(letter);
            this.GameEngine.AddMatchingLetters(letter);

            if (GameEngine.Tracker.GameOver)
            {
                UriHelper.NavigateTo("/gameOver/" + GameEngine.CurrentWord);
                return;
            }

            if(GameEngine.PlayingWord == GameEngine.CurrentWord)
            {
                await LoadNewWord();
            }
        }

        public async Task UseJoker()
        {
            GameEngine.UseJoker();
            if (GameEngine.PlayingWord == GameEngine.CurrentWord)
            {
                await LoadNewWord();
            }
        }

        private async Task LoadNewWord()
        {
            string currentWord = await GetWordFromDatabase();
            GameEngine.InitializeNewWord(currentWord);
        }

        private async Task<string> GetWordFromDatabase()
        {
            string level = await JsInterop.GetSessionStorageItem(nameof(WordDifficulty));
            string categoryId = await JsInterop.GetSessionStorageItem(GlobalConstants.CategoryIdentifierKey);
            this.Response = await this.ApiClient.GetRandomWord(level, categoryId);
            return this.Response.Data;
        }
    }
}
