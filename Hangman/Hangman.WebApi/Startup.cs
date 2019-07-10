using Hangman.Common;
using Hangman.Data;
using Hangman.Mappings;
using Hangman.Services;
using Hangman.Shared.InputModels.User;
using Hangman.WebApi.Authentication;
using Hangman.WebApi.Extensions;
using Hangman.WebApi.Infrastrucure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
			AutoMapperConfig.RegisterMappings(typeof(UserRegisterInputModel).Assembly);

			services.AddDbContext<ApplicationDbContext>(options => options
			.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddTransient<IHasher, Hasher>();
			services.AddTransient<TokenProvider>();

			services.AddScoped<UserPrincipal>();

			services.AddScoped<IWordService, WordService>();
			services.AddScoped<IUserService, UserService>();

			var authenticationSection = Configuration.GetSection("Authentication");
			services.Configure<AuthenticationSettings>(authenticationSection);

			var adminDataSection = Configuration.GetSection("AdminData");
			services.Configure<AdminData>(adminDataSection);

			services.AddCors();
			services.AddMvc(options => 
			{
				options.Filters.Add(typeof(AuthorizeFilter));

			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
			//Enable application custom tiken based authentication
			app.UseTokenBasedAuthentication();
			app.UseMvc();
		}
	}
}
