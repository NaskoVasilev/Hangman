using Hangman.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Hangman.Shared.InputModels.WordCategory
{
    public class WordCategoryCreateInputModel : IMapTo<Models.WordCategory>
    {
        [Required]
        public string Name { get; set; }
    }
}
