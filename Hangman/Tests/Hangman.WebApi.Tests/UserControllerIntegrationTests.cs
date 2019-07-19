using Hangman.Common;
using Hangman.Shared;
using Hangman.Shared.InputModels.User;
using Hangman.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hangman.WebApi.Tests
{
    public class UserControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public UserControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task RegisterEndpointShouldCreateUserWithValidData()
        {
            var email = Guid.NewGuid().ToString() + "@abv.bg";
            var username = Guid.NewGuid().ToString();
            var client = factory.CreateClient();
            var model = new UserRegisterInputModel { ConfirmPassword = "123456", Password = "123456", Email = email, Username = username };
            var response = await client.PostJsonAsync<ApiResponse<UserResponseModel>>("/api/user/register", model);
            Assert.True(response.IsOk);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task RegisterEndpointShouldReturnErrorWithSameUsernameOrEmail()
        {
            var client = factory.CreateClient();
            var model = new UserRegisterInputModel { ConfirmPassword = "123456", Password = "123456", Email = "admin@admin.bg", Username = "admin" };
            var response = await client.PostJsonAsync<ApiResponse<UserResponseModel>>("/api/user/register", model);
            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.NotNull(response.Errors);
            Assert.Equal(response.Errors.First(), ErrorMessages.UserWithTheSameUsernameOrEmailExists);
        }

        [Fact]
        public async Task RegisterEndpointShouldReturnErrorsWithImvalidData()
        {
            var client = factory.CreateClient();
            var model = new UserRegisterInputModel { ConfirmPassword = "123456", Password = "123455", Email = "adminain.bg", Username = "admsdsd" };
            var response = await client.PostJsonAsync<ApiResponse<UserResponseModel>>("/api/user/register", model);
            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.NotNull(response.Errors);
        }

        [Theory]
        [InlineData("inv@;idUs3rname", "P@@sW0rdfdgdgjdfgkljdf")]
        [InlineData("inv@;idUs3rname", "123456")]
        [InlineData("admin", "P@@sW0rdfdgdgjdfgkljdf1654a34$%^$^%")]
        public async Task LoginEndpointShouldReturnErrorWithInvalidUsernameOrPassword(string username, string password)
        {
            var client = factory.CreateClient();
            var model = new UserLoginInputModel { Username = username, Password = password};
            var response = await client.PostJsonAsync<ApiResponse<UserResponseModel>>("/api/user/login", model);
            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.Equal(response.Errors.First(), ErrorMessages.InvalidUserNamrOrPassword);
        }

        [Fact]
        public async Task LoginEndpointShouldReturnCorrectDataWithValidUsernameAndPassword()
        {
            var client = factory.CreateClient();
            var model = new UserLoginInputModel { Username = "admin", Password = "123456" };
            var response = await client.PostJsonAsync<ApiResponse<UserResponseModel>>("/api/user/login", model);
            Assert.True(response.IsOk);
            Assert.NotNull(response.Data);
        }
    }
}
