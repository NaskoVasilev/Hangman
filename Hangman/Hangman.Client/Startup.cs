using Hangman.Client.Infrastructure;
using Hangman.Logic;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hangman.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ApiClient>();
            services.AddSingleton<JsInterop>();
            services.AddSingleton<ApplicationState>();
            services.AddSingleton<GameEngine>();
            services.AddSingleton<GameTracker>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
