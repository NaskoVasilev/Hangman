using Hangman.Common;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hangman.Blazor.Infrastructure
{
	public class ApiClient : IApiClient
	{
		private readonly HttpClient httpClinet;

		public ApiClient(HttpClient httpClinet)
		{
			this.httpClinet = httpClinet;
		}

		public Task<string> GetRandomWord() => this.GetJson<string>("word/GetRandomWord");

		private async Task<T> GetJson<T>(string path)
		{
			string url = GlobalConstants.ApiBaseUrl + path;
			T result = await httpClinet.GetJsonAsync<T>(url);
			return result;
		}
	}
}
