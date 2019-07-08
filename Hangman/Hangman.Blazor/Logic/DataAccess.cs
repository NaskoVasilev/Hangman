using Hangman.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hangman.Blazor.Logic
{
	public class DataAccess : IDataAccess
	{
		private readonly HttpClient httpClient;

		public DataAccess(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<string> GetRandomWord()
		{
			string word = await httpClient.GetStringAsync(GlobalConstants.ApiBaseUrl + "word/getRandomWord");
			return word;
		}
	}
}
