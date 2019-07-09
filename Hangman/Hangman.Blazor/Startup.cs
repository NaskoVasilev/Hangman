using Hangman.Blazor.Infrastructure;
using Hangman.Logic;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hangman.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddTransient<IApiClient, ApiClient>();
			services.AddSingleton<Game>();
		}

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
