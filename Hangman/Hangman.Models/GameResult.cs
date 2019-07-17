using Hangman.Models.Enums;

namespace Hangman.Models
{
    public class GameResult : BaseModel<string>
    {
        public int Score { get; set; }

        public int GuessedWords { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int CategoryId { get; set; }
        public WordCategory Category { get; set; }

        public WordDifficulty Difficulty { get; set; }
    }
}
