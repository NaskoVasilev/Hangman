using Hangman.Common;
using Hangman.Services;
using Hangman.WebApi.Authentication;
using Hangman.WebApi.Infrastrucure.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Hangman.WebApi.Tests
{
    public class AuthenticationMiddlewareTests
    {
        private static readonly string secret = Guid.NewGuid().ToString();
        private static readonly string userId = Guid.NewGuid().ToString();

        [Fact]
        public async Task SetUserPrincipalDataWithValidToken()
        {
            string username = "atanas";
            string token = new TokenProvider(new Hasher()).GenerateToken(username, userId, secret);
            var user = await InvokeMiddleware(token);
            Assert.Equal(username, user.Username);
            Assert.Equal(userId, user.UserId);
        }

        [Fact]
        public async Task DoNotSetUserPrincipalDataWithoutToken()
        {
            var user = await InvokeMiddleware();
            Assert.Null(user.Username);
            Assert.Null(user.UserId);
        }

        [Fact]
        public async Task DoNotSetUserPrincipalDataWithTokenGeneratedWithDifferentKey()
        {
            string username = "atanas";
            string token = new TokenProvider(new Hasher()).GenerateToken(username, userId, Guid.NewGuid().ToString());
            var user = await InvokeMiddleware(token);
            Assert.Null(user.Username);
            Assert.Null(user.UserId);
        }

        [Fact]
        public async Task DoNotSetUserPrincipalDataWithTokenWithReplacedUserId()
        {
            string username = "atanas";
            string token = new TokenProvider(new Hasher()).GenerateToken(username, userId, secret);
            token = token.Replace(userId, Guid.NewGuid().ToString());
            var user = await InvokeMiddleware(token);
            Assert.Null(user.Username);
            Assert.Null(user.UserId);
        }

        [Fact]
        public async Task DoNotSetUserPrincipalDataWithTokenWithEditedSigniture()
        {
            string username = "atanas";
            string token = new TokenProvider(new Hasher()).GenerateToken(username, userId, secret);
            token += "s";
            var user = await InvokeMiddleware(token);
            Assert.Null(user.Username);
            Assert.Null(user.UserId);
        }

        private static async Task<UserPrincipal> InvokeMiddleware(string token = null)
        {
            IHasher hasher = new Hasher();
            var headerDictionary = new HeaderDictionary();
            if (token != null)
            {
                string tokenValue = @"Bearer " + token;
                headerDictionary.Add(AuthenticationMiddleware.AuthorizationHeaderKey, new StringValues(tokenValue));
            }

            var requestDelagateMock = new Mock<RequestDelegate>();
            AuthenticationMiddleware middleware = new AuthenticationMiddleware(requestDelagateMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.Request.Headers)
                .Returns(headerDictionary);

            var optionsMock = new Mock<IOptions<AuthenticationSettings>>();
            optionsMock.Setup(o => o.Value).Returns(new AuthenticationSettings() { Secret = secret });
            UserPrincipal user = new UserPrincipal();
            await middleware.InvokeAsync(httpContextMock.Object, hasher, optionsMock.Object, user);
            return user;
        }
    }
}
