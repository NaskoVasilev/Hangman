using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
    public class WordCategory : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
