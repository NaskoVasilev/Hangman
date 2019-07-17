using System;

namespace Hangman.Logic
{
    public static class ScoreEstimater
    {
        private const int MaxFailsCountWithoutLoosingPoints = 3;
        private const int DefaultWordPoints = 10;

        public static int CalculateWordScore(int wordDifficulty, int fails)
        {
            double score = DefaultWordPoints * wordDifficulty;
            if(fails > MaxFailsCountWithoutLoosingPoints)
            {
                double penaltyMultiplier = (fails - MaxFailsCountWithoutLoosingPoints) / 10.0;
                score *= (1 - penaltyMultiplier);
            }

            return (int)Math.Floor(score);
        }

        public static int CalculateBonusScore(int wordDifficulty, int guessedWords)
        {
            return wordDifficulty * guessedWords; 
        }
    }
}
