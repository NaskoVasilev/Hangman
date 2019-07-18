using Hangman.Services;
using Hangman.Shared;
using Hangman.Shared.InputModels.GameResult;
using Hangman.Shared.ResponseModels.GameResult;
using Hangman.WebApi.Authentication;
using Hangman.WebApi.Infrastrucure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    }
}
