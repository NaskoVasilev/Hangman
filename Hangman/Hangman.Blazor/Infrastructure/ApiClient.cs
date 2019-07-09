using Hangman.Common;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hangman.Blazor.Infrastructure
{
	public class ApiClient
	{
		private readonly HttpClient httpClinet;

		public ApiClient(HttpClient httpClinet)
		{
			this.httpClinet = httpClinet;
		}


		private Task<T> GetJson<T>(string path)
		{
			string url = GlobalConstants
			T result = await httpClinet.GetJsonAsync<T>(url);
		}
	}
}
