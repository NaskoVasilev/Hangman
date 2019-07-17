using System;

namespace Hangman.Logic
{
    public class GameTracker
    {
        private int fails;
        private int usedJokers;

        public const int MaxFails = 9;
        public const int MaxJokers = 1;

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

        public int UsedJokers
        {
            get => this.usedJokers;
            set
            {
                usedJokers = value;
                OnStateChange?.Invoke();
            }
        }

        public bool GameOver => this.Fails >= MaxFails;

        public bool HasAvailableJokers => this.UsedJokers < MaxJokers;

        public void Reset()
        {
            this.usedJokers = 0;
            this.fails = 0;
        }
    }
}
