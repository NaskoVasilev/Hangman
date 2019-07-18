using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangman.Data;
using Hangman.Mappings;
using Hangman.Models;
using Hangman.Shared;
using Hangman.Shared.InputModels.GameResult;
using Hangman.Shared.ResponseModels.GameResult;
using Microsoft.EntityFrameworkCore;

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

        public UserGameResultsResponseModel GetCurrentUserResults(string userId)
        {
            var results = this.context.GameResults
                .Where(x => x.UserId == userId);
            var currentUserResults = new UserGameResultsResponseModel
            {
                AverageScore = (int)results.Average(x => x.Score),
                GuessedWords = results.Sum(x => x.GuessedWords),
            };

            var username = this.context.Users.Find(userId).Username;
            currentUserResults.User = username;
            return currentUserResults;

        }

        public List<UserGameResultsResponseModel> GetTop20Users()
        {
            var results = this.context.GameResults
                .GroupBy(x => x.User.Username)
                .Select(x => new UserGameResultsResponseModel()
                {
                    AverageScore = (int)x.Average(r => r.Score),
                    GuessedWords = x.Sum(r => r.GuessedWords),
                    User = x.Key
                });

            var finalResults = results
                .OrderByDescending(x => x.AverageScore)
                .ThenByDescending(x => x.GuessedWords)
                .Take(20)
                .ToList();
            return finalResults; 
        }

        public List<GameResultsByCategory> GetUserResultsByCategories(string userId)
        {
            var user = this.context.Users
                .Include(x => x.GameResults)
                .FirstOrDefault(x => x.Id == userId);

            var results = user.GameResults
                .GroupBy(r => r.Category.Name)
                .Select(r => new GameResultsByCategory
                {
                    CategoryName = r.Key,
                    TotalScore = r.Sum(x => x.Score)
                })
                .OrderByDescending(x => x.TotalScore)
                .ToList();

            return results;
        }

        public IEnumerable<GameResultResponseModel> GetUserResultsGroupedByCategoryAndDifficulty(string userId)
        {
            var results = this.context.GameResults
                .Where(r => r.UserId == userId)
                .OrderBy(r => r.Category.Name)
                .ThenBy(r => r.Difficulty)
                .GroupBy(r => new { r.Category.Name, r.Difficulty })
                .Select(x => new GameResultResponseModel
                {
                    TotalScore = x.Sum(r => r.Score),
                    Difficulty = x.Key.Difficulty.ToString(),
                    CategoryName = x.Key.Name,
                    TotalGuessedWords = x.Sum(r => r.GuessedWords),
                })
                .ToList();

            return results;
        }
    }
}
