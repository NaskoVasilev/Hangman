using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class WordCategory : BaseModel<int>
    {
        public WordCategory()
        {
            this.GameResults = new List<GameResult>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<GameResult> GameResults { get; set; }
    }
}
