using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hangman.Logic.Tests
{
    public class ScoreEstimatorTests
    {
        [Theory]
        [InlineData("Easy", 3, 10)]
        [InlineData("Medium", 1, 20)]
        [InlineData("Hard", 0, 30)]
        [InlineData("Expert", 2, 40)]
        [InlineData("Expert", 5, 32)]
        [InlineData("Expert", 9, 16)]
        [InlineData("Easy", 9, 4)]
        [InlineData("Medium", 4, 18)]
        [InlineData("Hard", 6, 21)]
        public void CalculateWordScoreShouldWprdCorrectWithDifferentInputs(string wordLevel, int fails, int expectedScore)
        {
            int actualScore = ScoreEstimator.CalculateWordScore(wordLevel, fails);
            Assert.Equal(expectedScore, actualScore);
        }
    }
}
