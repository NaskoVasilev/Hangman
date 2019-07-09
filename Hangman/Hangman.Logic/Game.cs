using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Logic
{
	public class Game
	{
		public string CurrentWord { get; set; }

		public string GameWord { get; set; }

		public void InitializeNewWord(string word)
		{
			CurrentWord = word;
			GameWord = new string('_', word.Length);
		}
	}
}
