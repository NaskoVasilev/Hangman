using System;

namespace Hangman.Logic
{
    public class GameTracker
    {
        private int fails;
        private int availableJokers;
        private int totalScore;

        public const int MaxFails = 9;
        public const int JokersCountPerWord = 1;

        public GameTracker()
        {
            this.availableJokers = 0;
        }

        public event Action OnStateChange;

        public int Fails
        {
            get => this.fails;
            set
            {
                fails = value;
                OnStateChange?.Invoke();
            }
        }

        public int AvailableJokers
        {
            get => this.availableJokers;
            set
            {
                availableJokers = value;
                OnStateChange?.Invoke();
            }
        }

        public void IncreaseJokersCount()
        {
            this.AvailableJokers += JokersCountPerWord;
        }

        public int TotalScore
        {
            get => this.totalScore;
            set
            {
                totalScore = value;
                OnStateChange?.Invoke();
            }
        }

        public int GuessedWords { get; set; }

        public bool GameOver => this.Fails >= MaxFails;

        public bool HasAvailableJokers => this.AvailableJokers > 0;

        public void Reset()
        {
            this.fails = 0;
        }

        public void ResetScoreAndGuessedWords()
        {
            this.totalScore = 0;
            this.GuessedWords = 0;
        }
    }
}
