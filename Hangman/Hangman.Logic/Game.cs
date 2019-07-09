using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Logic
{
	public class Game
	{
		public string CurrentWord { get; set; }

		public string PlayingWord { get; set; }

		public void InitializeNewWord(string word)
		{
			CurrentWord = word;
			PlayingWord = new string('_', word.Length);
		}

		public void AddMatchingLetters(string letter)
		{
			Console.WriteLine(letter);
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
