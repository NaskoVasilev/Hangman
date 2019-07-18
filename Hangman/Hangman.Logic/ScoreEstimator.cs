using Hangman.Models.Enums;
using System;

namespace Hangman.Logic
{
    public static class ScoreEstimator
    {
        private const int MaxFailsCountWithoutLoosingPoints = 3;
        private const int DefaultWordPoints = 10;

        public static int CalculateWordScore(string wordLevel, int fails)
        {
            if(fails < 0)
            {
                throw new ArgumentException("Fails can not be negative number.");
            }

            int wordDifficulty = GetWordDifficulty(wordLevel);
            double score = DefaultWordPoints * wordDifficulty;
            if(fails > MaxFailsCountWithoutLoosingPoints)
            {
                double penaltyMultiplier = (fails - MaxFailsCountWithoutLoosingPoints) / 10.0;
                score *= (1 - penaltyMultiplier);
            }

            return (int)Math.Floor(score);
        }

        public static int CalculateBonusScore(string wordLevel, int guessedWords)
        {
            if(guessedWords < 0)
            {
                throw new ArgumentException("Guessed words can not be negative number.");
            }

            return GetWordDifficulty(wordLevel) * guessedWords; 
        }

        private static int GetWordDifficulty(string wordLevel)
        {
            if(Enum.TryParse<WordDifficulty>(wordLevel, ignoreCase: true, out WordDifficulty wordDifficulty))
            {
                return (int)wordDifficulty;
            }
            throw new ArgumentException("Invalid word level.");
        }
    }
}
