using Hangman.Common;
using Hangman.Mappings;
using Hangman.Models.Enums;
using Hangman.Shared.InputModels.WordCategory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Shared.InputModels.Word
{
    public class WordEditInputModel : IMapFrom<Models.Word>, IMapTo<Models.Word>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ModelValidations.WordMaxLength)]
        [MinLength(ModelValidations.ModelMinLength)]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public WordDifficulty WordDifficulty { get; set; }
    }
}
