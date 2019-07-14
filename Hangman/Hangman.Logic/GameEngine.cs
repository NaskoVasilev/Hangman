namespace Hangman.Logic
{
    public class GameEngine
	{
        public GameEngine(GameTracker tracker)
        {
            Tracker = tracker;
        }

        public string CurrentWord { get; private set; }

		public string PlayingWord { get; private set; }

        public GameTracker Tracker { get; }

        public void InitializeNewWord(string word)
		{
            Tracker.Reset();
			CurrentWord = word;
			PlayingWord = new string('_', word.Length);
		}

		public void AddMatchingLetters(string letter)
		{
            string initialPlayingWord = PlayingWord;
			for (int i = 0; i < CurrentWord.Length; i++)
			{
				if(CurrentWord[i].ToString() == letter)
				{
					PlayingWord = PlayingWord.Substring(0, i) + letter + PlayingWord.Substring(i + 1);
				}
			}

            if(initialPlayingWord == PlayingWord)
            {
                this.Tracker.Fails++;
            }
		}
	}
}
