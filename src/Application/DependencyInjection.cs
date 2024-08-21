using Application.RuleBehaviour;
using Application.UseCase;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IHolidayRule, WeekdayHolidayRule>();
			services.AddTransient<IHolidayRule, WeekendHolidayRule>();
			services.AddTransient<IHolidayRule, AnzacDayHolidayRule>();
			services.AddTransient<IHolidayRule, KingsBirthdayHolidayRule>();
			services.AddTransient<IHolidayRule, LabourDayHolidayRule>();
			services.AddTransient<IHolidayRule, EasterHolidayRule>();
			services.AddTransient<IHolidayRule, ChristmasHolidayRule>();

			services.AddTransient<IBusinessDayCounter, BusinessDayCounter>();

			return services;
		}
	}
}