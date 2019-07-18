using Hangman.Shared.InputModels.GameResult;
using System.Threading.Tasks;

namespace Hangman.Services
{
    public interface IGameReslultService
    {
        Task<bool> Create(GameResultInputModel model);
    }
}
