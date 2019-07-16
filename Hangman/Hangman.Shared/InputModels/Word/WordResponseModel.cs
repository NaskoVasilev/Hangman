using Hangman.Mappings;

namespace Hangman.Shared.InputModels.Word
{
    public class WordResponseModel : IMapFrom<Models.Word>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CategoryName { get; set; }

        public string WordDifficulty { get; set; }
    }
}
