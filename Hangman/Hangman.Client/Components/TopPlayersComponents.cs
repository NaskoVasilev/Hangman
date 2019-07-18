using Hangman.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class TopPlayersComponents : BaseHangmanComponent
    {
        public TopPlayersComponents()
        {
            this.Players = new List<UserGameResultsResponseModel>();
        }

        public List<UserGameResultsResponseModel> Players { get; set; }

        protected override async Task OnInitAsync()
        {
            var response = await ApiClient.GetTopPlayers();
            Players = response.Data;
        }
    }
}
