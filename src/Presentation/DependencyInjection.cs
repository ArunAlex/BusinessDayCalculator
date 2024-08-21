using Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;

namespace Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddControllers();
			services.Configure<PublicHolidayOptions>(Configuration.GetSection(nameof(PublicHolidayOptions)));

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendar API", Version = "v1" });

				// Add support for XML comments
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});

			return services;
		}
	}
}
