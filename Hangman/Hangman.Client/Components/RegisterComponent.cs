using Hangman.Client.Infrastructure;
using Hangman.Common;
using Hangman.Shared;
using Hangman.Shared.InputModels.User;
using Hangman.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class RegisterComponent : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public JsInterop JsInterop { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public IUriHelper UriHelper { get; set; }

        public ApiResponse<UserResponseModel> Response { get; set; }

        public UserRegisterInputModel InputModel { get; set; } = new UserRegisterInputModel();

        public async Task RegisterUser()
        {
            Console.WriteLine(InputModel.Username + ' ' + InputModel.Email);
            Console.WriteLine(InputModel.Password + ' ' + InputModel.ConfirmPassword);
            Response = await ApiClient.Register(InputModel);
            if(this.Response.IsOk)
            {
                //Remove this later, now is used only for debugging
                Console.WriteLine(Response.Data.Username);
                Console.WriteLine(Response.Data.Email);
                UriHelper.NavigateTo("/login");
            }
        }
    }
}
