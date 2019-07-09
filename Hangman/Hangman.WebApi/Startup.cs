using Hangman.Common;
using Hangman.Data;
using Hangman.Services;
using Hangman.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangman.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IWordService, WordService>();
			services.AddSingleton<IWordRepository, InMemoryWordRepository>();
			services.AddCors();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseDataSeeders();
			app.UseHttpsRedirection();
			app.UseCors(options => 
			{
				options.AllowAnyHeader();
				options.WithOrigins("http://localhost:54685");
			});
			app.UseMvc();
		}
	}
}
