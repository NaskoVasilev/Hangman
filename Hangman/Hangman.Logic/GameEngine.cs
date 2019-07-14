using System;
using System.Collections.Generic;
using System.Linq;

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

        public void UseJoker()
        {
            if(!this.Tracker.HasAvailableJokers)
            {
                return;
            }

            char letter = GetRandomLetterWithMinPriority();
            AddMatchingLetters(letter.ToString());
            this.Tracker.UsedJokers++;
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

        private char GetRandomLetterWithMinPriority()
        {
            Dictionary<char, int> lettersByOccurences = new Dictionary<char, int>();
            for (int i = 0; i < PlayingWord.Length; i++)
            {
                if (PlayingWord[i] == '_')
                {
                    char letter = CurrentWord[i];
                    if (!lettersByOccurences.ContainsKey(letter))
                    {
                        lettersByOccurences.Add(letter, 0);
                    }
                    lettersByOccurences[letter]++;
                }
            }

            var random = new Random();
            char minPriorotyRandomLetter = lettersByOccurences
                .OrderBy(l => l.Value)
                .ThenBy(r => random.Next())
                .First().Key;
            return minPriorotyRandomLetter;
        }
	}
}
