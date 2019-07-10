using Hangman.Mappings;
using Hangman.Models;

namespace Hangman.Shared.ResponseModels
{
    public class UserResponseModel : IMapFrom<ApplicationUser>
    {
        public string Username { get; set; }

        public string Email { get; set; }
    }
}
