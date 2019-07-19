using Hangman.Services;
using Hangman.Shared;
using Hangman.Shared.InputModels.GameResult;
using Hangman.Shared.ResponseModels.GameResult;
using Hangman.WebApi.Authentication;
using Hangman.WebApi.Infrastrucure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.WebApi.Controllers
{
    public class GameResultController : ApiController
    {
        private readonly IGameReslultService gameReslultService;
        private readonly UserPrincipal user;

        public GameResultController(IGameReslultService gameReslultService, UserPrincipal user)
        {
            this.gameReslultService = gameReslultService;
            this.user = user;
        }

        [Authorization]
        [HttpPost("[action]")]
        public async Task<ApiResponse<bool>> Create(GameResultInputModel model)
        {
            model.UserId = this.user.UserId;
            var result = await gameReslultService.Create(model);
            return new ApiResponse<bool>(result);
        }

        [Authorization]
        [HttpGet("[action]")]
        public ApiResponse<IEnumerable<GameResultResponseModel>> MyStatictics()
        {
            IEnumerable<GameResultResponseModel> results = gameReslultService.GetUserResultsGroupedByCategoryAndDifficulty(user.UserId);
            return new ApiResponse<IEnumerable<GameResultResponseModel>>(results);
        }

        [Authorization]
        [HttpGet("[action]")]
        public ApiResponse<List<UserGameResultsResponseModel>> GetTopPlayers()
        {
            List<UserGameResultsResponseModel> results = gameReslultService.GetTop20Users();
            //if(!results.Any(x => x.User == user.Username))
            //{
            //    UserGameResultsResponseModel userResults = gameReslultService.GetCurrentUserResults(user.UserId);
            //    results.Add(userResults);
            //}
            return new ApiResponse<List<UserGameResultsResponseModel>>(results);
        }

        [Authorization]
        [HttpGet("[action]")]
        public ApiResponse<List<GameResultsByCategory>> MyResultsByCategories()
        {
            List<GameResultsByCategory> results = gameReslultService.GetUserResultsByCategories(user.UserId);
            return new ApiResponse<List<GameResultsByCategory>>(results);
        }
    }
}
