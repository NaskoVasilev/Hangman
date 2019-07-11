﻿namespace Hangman.Client.Infrastructure
{
    public class ApplicationState
    {
        public string Username { get; set; }

        public string UserToken { get; set; }

        public bool IsLoggedIn => UserToken != null;

        public void CleareState()
        {
            this.Username = null;
            this.UserToken = null;
        }
    }
}
