using System;
using System.Threading.Tasks;
using Hangman.Logic;
using Hangman.Models.Enums;
using Hangman.Shared;
using Hangman.Shared.InputModels.GameResult;
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
            this.GameEngine.AddMatchingLetters(letter);

            if (GameEngine.Tracker.GameOver)
            {
                await FinishGame();
                return;
            }

            if (GameEngine.PlayingWord == GameEngine.CurrentWord)
            {
                TrackResultChanges();
                await LoadNewWord();
            }
        }

        public async Task FinishGame()
        {
            await ProcessGameResults();
            UriHelper.NavigateTo($"/gameOver/{GameEngine.CurrentWord}/{GameEngine.Tracker.GuessedWords}/{this.Level}/{this.CategoryId}");
            GameEngine.Tracker.ResetScoreAndGuessedWords();
        }

        private void TrackResultChanges()
        {
            this.GameEngine.Tracker.GuessedWords++;
            int wordsScore = ScoreEstimator.CalculateWordScore(this.Level, GameEngine.Tracker.Fails);
            this.GameEngine.Tracker.TotalScore += wordsScore;
        }

        private async Task ProcessGameResults()
        {
            int bonusScore = ScoreEstimator.CalculateBonusScore(this.Level, this.GameEngine.Tracker.GuessedWords);
            GameEngine.Tracker.TotalScore += bonusScore;
            await SaveGameResult();
        }

        public async Task UseJoker()
        {
            GameEngine.UseJoker();
            if (GameEngine.PlayingWord == GameEngine.CurrentWord)
            {
                TrackResultChanges();
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

        private async Task SaveGameResult()
        {
            var inputModel = new GameResultInputModel
            {
                CategoryId = int.Parse(this.CategoryId),
                Difficulty = (WordDifficulty)Enum.Parse(typeof(WordDifficulty), this.Level),
                GuessedWords = this.GameEngine.Tracker.GuessedWords,
                Score = this.GameEngine.Tracker.TotalScore
            };

            await this.ApiClient.CreateGameResult(inputModel);
        }
    }
}
