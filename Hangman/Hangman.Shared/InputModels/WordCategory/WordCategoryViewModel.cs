using Hangman.Mappings;

namespace Hangman.Shared.InputModels.WordCategory
{
    public class WordCategoryViewModel : IMapFrom<Models.WordCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
