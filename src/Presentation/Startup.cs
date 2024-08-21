using Application;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Web;

namespace Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IWebHostEnvironment HostingEnvironment { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			HostingEnvironment = env;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddWebServices(Configuration);
			services.AddApplicationServices(Configuration);

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendar API", Version = "v1" });

				// Add support for XML comments
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});

			services.AddHealthChecks();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IServiceProvider services)
		{
			ApplyEnvironmentBasedConfigurations(app, env);

			ApplyRoutingAndSecurity(app);

			ApplyAuthenticationAndAuthorization(app);

			ApplyEndpointsAndMiddleware(app, env);

			ApplySwagger(app);
		}


		#region Configure Methods

		protected virtual void ApplyEnvironmentBasedConfigurations(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				return;
			}

			app.UseHttpsRedirection();
			app.UseHsts();
		}

		private void ApplyRoutingAndSecurity(IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseCors("CorsPolicy");
		}

		protected virtual void ApplyAuthenticationAndAuthorization(IApplicationBuilder app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
		}

		private void ApplyEndpointsAndMiddleware(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
			app.UseHealthChecks("/health");
		}

		private void ApplySwagger(IApplicationBuilder app)
		{
			//Swagger Configure
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API");
			});
		}

		#endregion Configure Methods
	}
}
