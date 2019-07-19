using System;
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
        public void CalculateWordScoreShouldWorkCorrectWithDifferentInputs(string wordLevel, int fails, int expectedScore)
        {
            int actualScore = ScoreEstimator.CalculateWordScore(wordLevel, fails);
            Assert.Equal(expectedScore, actualScore);
        }

        [Theory]
        [InlineData("Easy", -1)]
        [InlineData("Hard", -5)]
        public void CalculateWordScoreShouldWorkThrowErrorWithNegativeFails(string wordLevel, int fails)
        {
            Assert.Throws<ArgumentException>(() => ScoreEstimator.CalculateWordScore(wordLevel, fails));
        }

        [Fact]
        public void CalculateWordScoreShouldThrowErrorWithInvalidWordLevel()
        {
            Assert.Throws<ArgumentException>(() => ScoreEstimator.CalculateWordScore("invalid", 4));
        }

        [Fact]
        public void CalculateWordScoreWorkCorrectWithWordLevelInUpperCase()
        {
            int actualResult = ScoreEstimator.CalculateBonusScore("expert", 5);
            Assert.Equal(20, actualResult);
        }

        [Theory]
        [InlineData("Easy", 10, 10)]
        [InlineData("Medium", 10, 20)]
        [InlineData("Expert", 10, 40)]
        [InlineData("Hard", 10, 30)]
        [InlineData("Hard", 1, 3)]
        [InlineData("Easy", 0, 0)]
        public void CalculateBonusScoreShouldWorkCorrectWithDifferentData(string wordLevel, int guessedWords, int expectedResult)
        {
            int actualResult = ScoreEstimator.CalculateBonusScore(wordLevel, guessedWords);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("Easy", -1)]
        [InlineData("Hard", -5)]
        public void CalculateBonusScoreShouldWorkThrowErrorWithNegativeGuessedWords(string wordLevel, int guessedWords)
        {
            Assert.Throws<ArgumentException>(() => ScoreEstimator.CalculateBonusScore(wordLevel, guessedWords));
        }


        [Fact]
        public void CalculateBonusScoreShouldThrowErrorWithInvalidWordLevel()
        {
            Assert.Throws<ArgumentException>(() => ScoreEstimator.CalculateBonusScore("invalid", 4));
        }

        [Fact]
        public void CalculateBonusScoreWorkCorrectWithWordLevelInUpperCase()
        {
            int actualResult = ScoreEstimator.CalculateBonusScore("hard", 5);
            Assert.Equal(15, actualResult);
        }
    }
}
