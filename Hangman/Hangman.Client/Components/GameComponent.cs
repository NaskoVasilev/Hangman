using System.Threading.Tasks;
using Hangman.Logic;
using Hangman.Shared;
using Microsoft.AspNetCore.Components;

namespace Hangman.Client.Components
{
    public class GameComponent : BaseHangmanComponent
    {
        [Inject]
        public GameEngine GameEngine { get; set; }

        public ApiResponse<string> Response { get; set; }

        [Parameter]
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Parameter]
        public string Level { get; set; }

        protected override async Task OnInitAsync()
        {
            this.GameEngine.Tracker.OnStateChange += this.StateHasChanged;
            var response = await ApiClient.GetCategoryNameById(this.CategoryId);
            this.CategoryName = response.Data;
            await LoadNewWord();
        }

        public async Task Check(char letter)
        {
            System.Console.WriteLine(letter);
            this.GameEngine.AddMatchingLetters(letter);

            if (GameEngine.Tracker.GameOver)
            {
                UriHelper.NavigateTo($"/gameOver/{GameEngine.CurrentWord}/{GameEngine.Tracker.GuessedWords}");
                return;
            }

            if(GameEngine.PlayingWord == GameEngine.CurrentWord)
            {
                this.GameEngine.Tracker.GuessedWords++;
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
            this.Response = await this.ApiClient.GetRandomWord(this.Level, this.CategoryId);
            return this.Response.Data;
        }
    }
}
