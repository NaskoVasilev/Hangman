﻿using Hangman.Shared;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Hangman.Client.Infrastructure
{
    public class ApplicationState
    {
        private readonly JsInterop jsInterop;
        private string username;
        private string userToken;

        public ApplicationState(JsInterop jsInterop)
        {
            this.jsInterop = jsInterop;
        }

        public string Username
        {
            get => this.username;
            set
            {
                this.username = value;
                this.OnUserDataChange?.Invoke();
            }
        }

        public string UserToken
        {
            get => this.userToken;
            set
            {
                this.userToken = value;
                this.OnUserDataChange?.Invoke();
            }
        }

        public event Action OnUserDataChange;

        public bool IsLoggedIn => UserToken != null;

        public void CleareState()
        {
            this.Username = null;
            this.UserToken = null;
        }

        public async Task RestoreFromLocalStorage()
        {
            string token = await jsInterop.GetToken();
            if(!string.IsNullOrEmpty(token))
            {
                string[] splittedToken = token.Split('.');
                if(splittedToken.Length == 2)
                {
                    var user = JsonConvert.DeserializeObject<TokenUserData>(splittedToken[0]);
                    this.Username = user.Username;
                    this.UserToken = splittedToken[1];
                }
            }
        }
    }
}