namespace Hangman.Client.Infrastructure
{
    public class ApplicationState
    {
        public string Username { get; set; }

        public string UserToken { get; set; }

        public bool IsLoggedIn => UserToken != null;
    }
}
