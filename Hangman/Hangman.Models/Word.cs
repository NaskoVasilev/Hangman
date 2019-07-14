using Hangman.Common;
using Hangman.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Models
{
	public class Word : BaseModel<int>
	{
		[Required]
		[MaxLength(ModelValidations.WordMaxLength)]
		public string Content { get; set; }

        public int CategoryId { get; set; }
        public WordCategory Category { get; set; }

        public WordDifficulty WordDifficulty { get; set; }
	}
}
