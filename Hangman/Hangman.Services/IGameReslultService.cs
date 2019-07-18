using Hangman.Shared.InputModels.GameResult;
using Hangman.Shared.ResponseModels.GameResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Services
{
    public interface IGameReslultService
    {
        Task<bool> Create(GameResultInputModel model);

        IEnumerable<GameResultResponseModel> GetUserResultsGroupedByCategoryAndDifficulty(string userId);
    }
}
