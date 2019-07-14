using System;

namespace Hangman.Logic
{
    public class GameTracker
    {
        public const int MaxFails = 5;

        public int Fails { get; set; }

        public bool GameOver => this.Fails >= MaxFails;

        public void Reset()
        {
            this.Fails = 0;
        }
    }
}
