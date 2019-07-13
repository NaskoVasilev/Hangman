using Hangman.Client.Infrastructure;
using Hangman.Shared;
using Hangman.Shared.InputModels.User;
using Hangman.Shared.ResponseModels;
using System;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class RegisterComponent : BaseHangmanComponent
    {
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
            else
            {
                foreach (var erro in Response.Errors)
                {
                    Console.WriteLine(erro);
                }
            }
        }
    }
}
