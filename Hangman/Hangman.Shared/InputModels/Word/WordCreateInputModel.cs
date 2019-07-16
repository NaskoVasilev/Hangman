using Hangman.Common;
using Hangman.Mappings;
using Hangman.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Shared.InputModels.Word
{
    public class WordCreateInputModel : IMapTo<Models.Word>
    {
        [Required]
        [MaxLength(ModelValidations.WordMaxLength)]
        [MinLength(ModelValidations.ModelMinLength)]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public WordDifficulty WordDifficulty { get; set; }
    }
}
