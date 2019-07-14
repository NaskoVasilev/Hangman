using Hangman.Mappings;

namespace Hangman.Shared.ResponseModels.WordCategory
{
    public class WordCategoryResponseModel : IMapFrom<Models.WordCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
