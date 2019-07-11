using Xunit;

namespace Hangman.Logic.Tests
{
    public class GameEngineTests
    {
        [Fact]
        public void AssertIntializeNewWordSetCurrentWordPropertyAndPlayingWordProperty()
        {
            GameEngine gameEngine = new GameEngine();
            string word = "Testing";
            string hiddenWord = new string('_', word.Length);
            gameEngine.InitializeNewWord(word);
            Assert.Equal(word, gameEngine.CurrentWord);
            Assert.Equal(hiddenWord, gameEngine.PlayingWord);
        }

        [Theory]
        [InlineData("a", "atanas", "a_a_a_")]
        [InlineData("a", "bsdww", "_____")]
        [InlineData("e", "test", "_e__")]
        [InlineData("e", "teestee", "_ee__ee")]
        [InlineData("b", "blob", "b__b")]
        public void AddMattchingLettersAddOnlyOneLetterCorrectly(string letter, string word, string expectedHiddenWord)
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.InitializeNewWord(word);
            gameEngine.AddMatchingLetters(letter);
            Assert.Equal(expectedHiddenWord, gameEngine.PlayingWord);
        }

        [Fact]
        public void AddMattchingLettersDoNothingWithInvalidData()
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.InitializeNewWord("test");
            gameEngine.AddMatchingLetters("");
            gameEngine.AddMatchingLetters("test");
            gameEngine.AddMatchingLetters("es");
            gameEngine.AddMatchingLetters("sd");
            Assert.Equal("____", gameEngine.PlayingWord);
        }

       [Fact]
        public void AddMattchingLettersAddMoreLetterCorrectly()
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.InitializeNewWord("documentation");
            gameEngine.AddMatchingLetters("d");
            gameEngine.AddMatchingLetters("n");
            gameEngine.AddMatchingLetters("o");
            gameEngine.AddMatchingLetters("c");
            gameEngine.AddMatchingLetters("t");
            gameEngine.AddMatchingLetters("d");
            string expectedResult = "doc___nt_t_on";
            Assert.Equal(expectedResult, gameEngine.PlayingWord);
        }

        [Fact]
        public void AddMattchingLettersFillAllWordWithValidData()
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.InitializeNewWord("test");
            gameEngine.AddMatchingLetters("t");
            gameEngine.AddMatchingLetters("a");
            gameEngine.AddMatchingLetters("e");
            gameEngine.AddMatchingLetters("s");
            string expectedResult = "test";
            Assert.Equal(expectedResult, gameEngine.PlayingWord);
        }
    }
}
