using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangman.Data;
using Hangman.Mappings;
using Hangman.Models;
using Hangman.Shared.InputModels.GameResult;
using Hangman.Shared.ResponseModels.GameResult;

namespace Hangman.Services
{
    public class GameResultService : IGameReslultService
    {
        private readonly ApplicationDbContext context;

        public GameResultService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(GameResultInputModel model)
        {
            var gameResult = model.To<GameResult>();
            await context.GameResults.AddAsync(gameResult);
            await context.SaveChangesAsync();
            return true;
        }

        public IEnumerable<GameResultResponseModel> GetUserResultsGroupedByCategoryAndDifficulty(string userId)
        {
            var results = this.context.GameResults
                .Where(r => r.UserId == userId)
                .GroupBy(r => new { r.CategoryId, r.Difficulty })
                .Select(x => new GameResultResponseModel
                {
                    TotalScore = x.Sum(r => r.Score),
                    Difficulty = x.Key.Difficulty.ToString(),
                    CategoryName = x.Key.CategoryId.ToString(),
                    TotalGuessedWords = x.Sum(r => r.GuessedWords),
                })
                .OrderBy(r => r.CategoryName)
                .ThenBy(r => r.Difficulty)
                .ToList();

            return results;
        }
    }
}
