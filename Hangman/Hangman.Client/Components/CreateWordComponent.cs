using Hangman.Shared;
using Hangman.Shared.InputModels.Word;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class CreateWordComponent : BaseHangmanComponent
    {
        public CreateWordComponent()
        {
            this.Model = new WordCreateInputModel();
        }

        public ApiResponse<bool> Response { get; set; }

        public WordCreateInputModel Model { get; set; }

        public async Task CreateHandler()
        {
            this.Response = await this.ApiClient.CreateWord(this.Model);
            if(Response.IsOk)
            {
                //TODO: navigate to all words page
                UriHelper.NavigateTo("/");
            }
        }
    }
}
