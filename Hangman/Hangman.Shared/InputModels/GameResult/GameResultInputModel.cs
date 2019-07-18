using AutoMapper;
using Hangman.Mappings;
using Hangman.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Shared.InputModels.GameResult
{
    public class GameResultInputModel : IMapTo<Models.GameResult>
    {
        public int Score { get; set; }

        public int GuessedWords { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public WordDifficulty Difficulty { get; set; }
    }
}
