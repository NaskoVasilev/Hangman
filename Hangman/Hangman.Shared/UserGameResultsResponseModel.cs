namespace Hangman.Shared
{
    public class UserGameResultsResponseModel
    {
        public string User { get; set; }

        public int GuessedWords { get; set; }

        public int AverageScore { get; set; }
    }
}
