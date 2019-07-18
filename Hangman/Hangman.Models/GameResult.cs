using Hangman.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class GameResult : BaseModel<string>
    {
        public int Score { get; set; }

        public int GuessedWords { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int CategoryId { get; set; }
        public WordCategory Category { get; set; }

        public WordDifficulty Difficulty { get; set; }
    }
}
