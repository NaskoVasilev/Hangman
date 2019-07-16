using Hangman.Shared.InputModels.Word;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class AllWordsComponent : BaseHangmanComponent
    {
        public AllWordsComponent()
        {
            this.Words = new List<WordResponseModel>();
        }

        public IEnumerable<WordResponseModel> Words { get; set; }

        protected override async Task OnInitAsync()
        {
            var response = await ApiClient.GetAllWords();
            if(response.IsOk)
            {
                this.Words = response.Data;
            }
        }
    }
}
