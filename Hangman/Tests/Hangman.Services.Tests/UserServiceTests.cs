using Hangman.Common;
using Hangman.Data;
using Hangman.Models;
using Hangman.Shared.InputModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Hangman.Services.Tests
{
    public class UserServiceTests : IClassFixture<MappingExecutor>
    {
        private readonly IUserService userService;
        private readonly IHasher hasher;
        private readonly ApplicationDbContext context;

        public UserServiceTests()
        {
            this.hasher = new Hasher();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            this.context = new ApplicationDbContext(options);
            this.userService = new UserService(context, hasher);
        }

        [Fact]
        public async Task CreateUserWithCorrectDataShouldReturnApplicationUserWithGuidId()
        {
            string email = "atanas@abv.bg";
            string username = "atanas";
            string password = "dskjgfjksdgds";
            var user = await userService.CreateUser(new UserRegisterInputModel { Email = email, Username = username, Password = password });
            Assert.NotNull(user.Id);
            Assert.Equal(username, user.Username);
            Assert.Equal(email, user.Email);
            Assert.Equal(this.hasher.Hash(password), user.Password);
        }

        [Theory]
        [InlineData("nasko", "nasko@abv.bg", false)]
        [InlineData("testUser1", "testUser1@abv.bg", true)]
        [InlineData("testUser1", "testUser2@abv.bg", true)]
        [InlineData("testUser2", "testUser1@abv.bg", true)]
        public void UserWithTheSameUsernameOrEmailExistsShouldReturnWorkCorrect(string username, string email, bool expected)
        {
            AddTestData();
            bool result = userService.UserWithTheSameUsernameOrEmailExists(username, email);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetUserByUserNameAndPasswordShouldReturnUser()
        {
            AddTestData();
            var result = userService.GetUserByUserNameAndPassword("testUser1", "basePassword1");
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("testUser2", "basePassword3")]
        [InlineData("testUsdfser2", "basePasdfssword3")]
        public async Task GetUserByUserNameAndPasswordShouldReturnNullWithInvalidData(string username, string password)
        {
            AddTestData();
            var result = await userService.GetUserByUserNameAndPassword(username, password);
            Assert.Null(result);
        }

        [Fact]
        public async Task AfterCreateGetByIdShoudReturnSameUser()
        {
            string email = "nasko@abv.bg";
            string username = "nasko";
            string password = "nasko";
            var user = await userService.CreateUser(new UserRegisterInputModel { Email = email, Username = username, Password = password });
            var userFromDb = userService.GetById(user.Id);
            Assert.Equal(user.Id, userFromDb.Id);
            Assert.Equal(user.Password, userFromDb.Password);
            Assert.Equal(user.Username, userFromDb.Username);
            Assert.Equal(user.Email, userFromDb.Email);
        }

        [Fact]
        public async Task IsInRoleShouldReturnTrueWithValidData()
        {
            string roleName = "Admin";
            var user = await userService.CreateUser(new UserRegisterInputModel { Email = "sdfds@sdfd.bg", Username = "sssaddf", Password = "sdfdsfdsf" });
            var role = new ApplicationRole() { Name = roleName };
            await context.Roles.AddAsync(role);
            await context.UserRoles.AddAsync(new ApplicationUserRole { RoleId = role.Id, UserId = user.Id });
            await context.SaveChangesAsync();

            var result = userService.IsInRole(roleName, user.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task IsInRoleShouldReturnFalseWithInvalidRoleName()
        {
            string roleName = "Admnin";
            var user = await userService.CreateUser(new UserRegisterInputModel { Email = "sdfds@sdfd.bg", Username = "sssaddf", Password = "sdfdsfdsf" });
            var role = new ApplicationRole() { Name = "Player" };
            await context.Roles.AddAsync(role);
            await context.UserRoles.AddAsync(new ApplicationUserRole { RoleId = role.Id, UserId = user.Id });
            await context.SaveChangesAsync();

            var result = userService.IsInRole(roleName, user.Id);
            Assert.False(result);
        }

        [Fact]
        public async Task IsInRoleShouldReturnFalseWithInvalidUserId()
        {
            string roleName = "Admin";
            var role = new ApplicationRole() { Name = roleName };
            await context.Roles.AddAsync(role);
            await context.UserRoles.AddAsync(new ApplicationUserRole { RoleId = role.Id, UserId = Guid.NewGuid().ToString() });
            await context.SaveChangesAsync();

            var result = userService.IsInRole(roleName, Guid.NewGuid().ToString());
            Assert.False(result);
        }

        private IEnumerable<ApplicationUser> GetTestData()
        {
            string username = "testUser";
            string basePassword = "basePassword";
            for (int i = 1; i < 4; i++)
            {
                yield return new ApplicationUser
                {
                    Email = username + i + "@abv.bg",
                    Username = username + i,
                    Password = this.hasher.Hash(basePassword + i)
                };
            }
        }

        private void AddTestData()
        {
            context.Users.AddRange(GetTestData());
            context.SaveChanges();
        }
    }
}
