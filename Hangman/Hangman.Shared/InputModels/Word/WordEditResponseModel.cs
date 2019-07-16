using Hangman.Mappings;
using Hangman.Models.Enums;
using Hangman.Shared.InputModels.WordCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hangman.Shared.InputModels.Word
{
    public class WordEditResponseModel : IMapFrom<Models.Word>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public WordDifficulty WordDifficulty { get; set; }

        public IEnumerable<string> WordDifficultes { get; set; }

        public IEnumerable<WordCategoryViewModel> WordCategories { get; set; }
    }
}
