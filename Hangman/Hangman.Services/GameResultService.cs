using System.Threading.Tasks;
using Hangman.Data;
using Hangman.Mappings;
using Hangman.Models;
using Hangman.Shared.InputModels.GameResult;

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
    }
}
