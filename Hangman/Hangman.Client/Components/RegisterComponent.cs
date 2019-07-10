using Hangman.Shared.InputModels.User;
using Microsoft.AspNetCore.Components;
using System;

namespace Hangman.Client.Components
{
    public class RegisterComponent : ComponentBase
    {
        public UserRegisterInputModel InputModel { get; set; } = new UserRegisterInputModel();

        public void RegisterUser()
        {
            Console.WriteLine(InputModel.Username + ' ' + InputModel.Email);
            Console.WriteLine(InputModel.Password + ' ' + InputModel.ConfirmPassword);
        }
    }
}
