using Xunit;

namespace Hangman.Logic.Tests
{
    public class GameEngineTests
    {
        [Fact]
        public void AssertIntializeNewWordSetCurrentWordPropertyAndPlayingWordProperty()
        {
            GameEngine gameEngine = new GameEngine(new GameTracker());
            string word = "Testing";
            string hiddenWord = new string('_', word.Length);

            gameEngine.InitializeNewWord(word);

            Assert.Equal(word, gameEngine.CurrentWord);
            Assert.Equal(hiddenWord, gameEngine.PlayingWord);
        }

        [Fact]
        public void AssertIntializeNewWordResetTrackerProperties()
        {
            GameTracker tracker = new GameTracker
            {
                Fails = 4,
                UsedJokers = 1
            };
            GameEngine gameEngine = new GameEngine(tracker);

            gameEngine.InitializeNewWord("asdasdsa");

            Assert.Equal(0, tracker.Fails);
            Assert.Equal(0, tracker.UsedJokers);
        }

        [Theory]
        [InlineData('a', "atanas", "a_a_a_")]
        [InlineData('a', "bsdww", "_____")]
        [InlineData('e', "test", "_e__")]
        [InlineData('e', "teestee", "_ee__ee")]
        [InlineData('b', "blob", "b__b")]
        public void AddMattchingLettersAddOnlyOneLetterCorrectly(char letter, string word, string expectedHiddenWord)
        {
            GameEngine gameEngine = new GameEngine(new GameTracker());

            gameEngine.InitializeNewWord(word);
            gameEngine.AddMatchingLetters(letter);

            Assert.Equal(expectedHiddenWord, gameEngine.PlayingWord);
        }

       [Fact]
        public void AddMattchingLettersAddMoreLetterCorrectly()
        {
            GameEngine gameEngine = new GameEngine(new GameTracker());
            gameEngine.InitializeNewWord("documentation");
            gameEngine.AddMatchingLetters('d');
            gameEngine.AddMatchingLetters('n');
            gameEngine.AddMatchingLetters('o');
            gameEngine.AddMatchingLetters('c');
            gameEngine.AddMatchingLetters('t');
            gameEngine.AddMatchingLetters('d');
            string expectedResult = "doc___nt_t_on";
            Assert.Equal(expectedResult, gameEngine.PlayingWord);
        }

        [Fact]
        public void AddMattchingLettersFillAllWordWithValidData()
        {
            GameEngine gameEngine = new GameEngine(new GameTracker());

            gameEngine.InitializeNewWord("test");
            gameEngine.AddMatchingLetters('t');
            gameEngine.AddMatchingLetters('a');
            gameEngine.AddMatchingLetters('e');
            gameEngine.AddMatchingLetters('s');
            string expectedResult = "test";

            Assert.Equal(expectedResult, gameEngine.PlayingWord);
        }

        [Fact]
        public void AddMatchingLetters_WithInvalidLetter_ShouldIncreaseFailsCount()
        {
            GameTracker tracker = new GameTracker();
            GameEngine gameEngine = new GameEngine(tracker);
            gameEngine.InitializeNewWord("test");
            gameEngine.AddMatchingLetters('a');
            gameEngine.AddMatchingLetters('e');
            gameEngine.AddMatchingLetters('s');
            gameEngine.AddMatchingLetters('r');
            gameEngine.AddMatchingLetters('f');
            Assert.Equal(3, tracker.Fails);
        }

        [Fact]
        public void GameOver_WithInvalidFiveInvalidAttemps_BecomesFalse()
        {
            GameTracker tracker = new GameTracker();
            GameEngine gameEngine = new GameEngine(tracker);
            gameEngine.InitializeNewWord("tet");
            gameEngine.AddMatchingLetters('a');
            gameEngine.AddMatchingLetters('e');
            gameEngine.AddMatchingLetters('s');
            gameEngine.AddMatchingLetters('r');
            gameEngine.AddMatchingLetters('f');
            for (int i = 0; i < GameTracker.MaxFails - 4; i++)
            {
                gameEngine.AddMatchingLetters('b');
            }

            Assert.True(tracker.GameOver);
        }

        [Fact]
        public void GetRandomLetterWithMinPriority_WithLetterWithOneOccurence_ShouldReturnThisLetter()
        {
            GameTracker tracker = new GameTracker();
            GameEngine gameEngine = new GameEngine(tracker);
            gameEngine.InitializeNewWord("tet");
            gameEngine.UseJoker();
            Assert.True(gameEngine.PlayingWord[1] == 'e');
            Assert.True(tracker.UsedJokers == 1);
        }

        [Fact]
        public void GetRandomLetterWithMinPriority_WithWordWithHasLettersWithTwoOccurenceOrMore_ShouldReturnLetterWithTwoOccurences()
        {
            GameTracker tracker = new GameTracker();
            GameEngine gameEngine = new GameEngine(tracker);
            gameEngine.InitializeNewWord("teetttaaasssbbb");
            gameEngine.UseJoker();
            Assert.True(gameEngine.PlayingWord[1] == 'e');
            Assert.True(gameEngine.PlayingWord[2] == 'e');
            Assert.True(tracker.UsedJokers == 1);
        }
    }
}
