using Hangman.Models.Enums;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class IndexComponent : BaseHangmanComponent
    {
        public bool ShouldRenderCategories { get; set; }

        public WordDifficulty Level { get; set; } = WordDifficulty.Easy;

        public int CategoryId { get; set; }

        public void StartGame()
        {
            UriHelper.NavigateTo($"/game/{this.Level.ToString()}/{this.CategoryId}");
        }

        protected override async Task OnInitAsync()
        {
            this.ApplicationState.OnUserDataChange += this.StateHasChanged;
            if((await JsInterop.GetToken()) != null)
            {
                ShouldRenderCategories = true;
            }
            base.OnInit();
        }
    }
}
