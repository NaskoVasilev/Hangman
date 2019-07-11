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
        [Fact]
        public async Task SetUserProncipalDataWithValidToken()
        {
            IHasher hasher = new Hasher();
            string userId = Guid.NewGuid().ToString();
            string username = "atanas";
            string secret = "somesupersecretstring";
            string token = new TokenProvider(hasher).GenerateToken(username, userId, secret);

            string tokenValue = @"Bearer " + token;
            var requestDelagateMock = new Mock<RequestDelegate>();
            AuthenticationMiddleware middleware = new AuthenticationMiddleware(requestDelagateMock.Object);
            var headerDictionary = new HeaderDictionary();
            headerDictionary.Add(AuthenticationMiddleware.AuthorizationHeaderKey, new StringValues(tokenValue));
            
             var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.Request.Headers)
                .Returns(headerDictionary);

                var optionsMock = new Mock<IOptions<AuthenticationSettings>>();
            optionsMock.Setup(o => o.Value).Returns(new AuthenticationSettings() { Secret = "ajshdsjk3whewdsjkfhds" });
            UserPrincipal user = new UserPrincipal();
            await middleware.InvokeAsync(httpContextMock.Object, hasher, optionsMock.Object, user);

            Assert.Equal(username, user.Username);
            Assert.Equal(userId, user.UserId);
        }
    }
}
