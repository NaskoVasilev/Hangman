using Hangman.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Shared.ResponseModels.GameResult
{
    public class GameResultResponseModel : IMapFrom<Models.GameResult>
    {
        public int TotalScore { get; set; }

        public int TotalGuessedWords { get; set; }

        public string CategoryName { get; set; }

        public string Difficulty { get; set; }
    }
}
