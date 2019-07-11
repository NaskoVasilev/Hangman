using Hangman.Data;
using Hangman.Mappings;
using Hangman.Shared.InputModels.User;
using Hangman.Shared.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hangman.Services.Tests
{
    public class MappingExecutor
    {
        public MappingExecutor()
        {
            //Congigure mappings
            AutoMapperConfig.RegisterMappings(typeof(UserRegisterInputModel).Assembly, typeof(UserResponseModel).Assembly);
        }
    }
}
