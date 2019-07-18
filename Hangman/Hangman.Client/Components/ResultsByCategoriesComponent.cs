using Hangman.Shared.ResponseModels.GameResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class ResultsByCategoriesComponent : BaseHangmanComponent
    {
        public ResultsByCategoriesComponent()
        {
            this.Results = new List<GameResultsByCategory>();
        }

        public List<GameResultsByCategory> Results { get; set; }

        protected override async Task OnInitAsync()
        {
            var response = await ApiClient.UserResultsByCategories();
            Results = response.Data;
        }
    }
}
