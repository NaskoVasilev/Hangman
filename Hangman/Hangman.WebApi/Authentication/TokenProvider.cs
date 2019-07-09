using Hangman.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.WebApi.Authentication
{
	public class TokenProvider
	{
		private readonly IHasher hasher;

		public TokenProvider(IHasher hasher)
		{
			this.hasher = hasher;
		}

		public string GenerateToken(string username, string userId, string key)
		{
			if(string.IsNullOrEmpty(key))
			{
				throw new ArgumentException("The key is required.");
			}

			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentException("The username is required.");
			}

			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("The userId is required.");
			}

			var userObject = new
			{
				username,
				userId
			};

			string json = JsonConvert.SerializeObject(userObject);
			string signature = hasher.Hash(userId + key);
			return json + "." + signature;
		}
	}
}
