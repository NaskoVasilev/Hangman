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
	}
}
