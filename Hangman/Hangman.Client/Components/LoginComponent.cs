using Hangman.Client.Infrastructure;
using Hangman.Shared;
using Hangman.Shared.InputModels.User;
using Hangman.Shared.ResponseModels;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class LoginComponent : BaseHangmanComponent
    {
        public UserLoginInputModel InputModel { get; set; } = new UserLoginInputModel();

        public ApiResponse<AuthenticatedUserResponseModel> Response { get; set; }

        public async Task LoginUser()
        {
            System.Console.WriteLine(InputModel.Username + " " + InputModel.Password);
            this.Response = await ApiClient.Login(InputModel);

            if(this.Response.IsOk)
            {
                await JsInterop.SaveToken(Response.Data.UserToken);
                ApplicationState.Username = Response.Data.Username;
                ApplicationState.UserToken = Response.Data.UserToken;
                this.UriHelper.NavigateTo("/");
            }
        }
    }
}
