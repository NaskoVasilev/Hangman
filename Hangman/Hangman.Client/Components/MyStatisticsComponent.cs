using Hangman.Shared.ResponseModels.GameResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class MyStatisticsComponent : BaseHangmanComponent
    {
        public MyStatisticsComponent()
        {
            this.GameResults = new List<GameResultResponseModel>();
        }

        public IEnumerable<GameResultResponseModel> GameResults { get; set; }

        protected override async Task OnInitAsync()
        {
            var response = await ApiClient.GetMyStatistics();
            if(response.IsOk)
            {
                GameResults = response.Data;
            }
        }
    }
}
