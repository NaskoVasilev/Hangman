using Hangman.Shared.InputModels.User;
using Microsoft.AspNetCore.Components;

namespace Hangman.Client.Components
{
    public class LoginComponent : ComponentBase
    {
        public UserLoginInputModel InputModel { get; set; } = new UserLoginInputModel();

        public void LoginUser()
        {
            System.Console.WriteLine(InputModel.Username + " " + InputModel.Password);
        }
    }
}
