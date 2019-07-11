using System;


namespace Hangman.Logic
{
    public class GameEngine
	{
		public string CurrentWord { get; private set; }

		public string PlayingWord { get; private set; }

		public void InitializeNewWord(string word)
		{
			CurrentWord = word;
			PlayingWord = new string('_', word.Length);
		}

		public void AddMatchingLetters(string letter)
		{
			for (int i = 0; i < CurrentWord.Length; i++)
			{
				if(CurrentWord[i].ToString() == letter)
				{
					PlayingWord = PlayingWord.Substring(0, i) + letter + PlayingWord.Substring(i + 1);
				}
			}
		}
	}
}
