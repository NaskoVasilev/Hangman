using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Hangman.Client.Infrastructure
{
    public class JsInterop
    {
        private readonly IJSRuntime jsRuntime;

        public JsInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<string> GetToken()
        {
            return await jsRuntime.InvokeAsync<string>("tokenManager.get");
        }

        public async Task<bool> SaveToken(string token)
        {
            return await jsRuntime.InvokeAsync<bool>("tokenManager.save", token);
        }

        public async Task<bool> RemoveToken()
        {
            return await jsRuntime.InvokeAsync<bool>("tokenManager.remove");
        }
    }
}
