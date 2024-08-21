using Domain.Common;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddControllers();
			services.Configure<PublicHolidayOptions>(Configuration.GetSection(nameof(PublicHolidayOptions)));

			return services;
		}
	}
}
