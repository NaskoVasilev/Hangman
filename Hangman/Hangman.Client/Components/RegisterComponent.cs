using Hangman.Common;
using Hangman.Shared.InputModels.User;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class RegisterComponent : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        public UserRegisterInputModel InputModel { get; set; } = new UserRegisterInputModel();

        public async Task RegisterUser()
        {
            await HttpClient.PostJsonAsync(GlobalConstants.ApiBaseUrl + "user/register", InputModel);
            Console.WriteLine(InputModel.Username + ' ' + InputModel.Email);
            Console.WriteLine(InputModel.Password + ' ' + InputModel.ConfirmPassword);
        }
    }
}
