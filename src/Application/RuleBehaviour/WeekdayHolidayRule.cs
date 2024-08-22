using Domain.Common;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.RuleBehaviour
{
	/// <summary>
	/// Process date is if holiday on weekday (New years and Australia day) 
	/// </summary>
	public class WeekdayHolidayRule : IHolidayRule
	{
		private readonly ILogger<WeekdayHolidayRule> _logger;

		public WeekdayHolidayRule(ILogger<WeekdayHolidayRule> logger)
		{
			_logger = logger;
		}

		public string CountryCode => "AU";

		public bool ProcessRule(DateTime date)
		{
			_logger.LogInformation($"Invoke Holidays on weekday rule {date.ToString("dd/MM/yyyy")}");
			var holidays = GetSameDayHolidays(date.Year);

			var weekDayHolidays = holidays.Where(h => h.Date.DayOfWeek != DayOfWeek.Sunday || h.Date.DayOfWeek != DayOfWeek.Saturday);

			return weekDayHolidays == null || weekDayHolidays.Any(p => p.Date.Date == date.Date);
		}

		private List<Holiday> GetSameDayHolidays(int year)
		{
			return new List<Holiday>()
			{
				new Holiday() { Name= "New Year", Date = new DateTime(year, 1,1)  },
				new Holiday() { Name= "Australia Day", Date = new DateTime(year, 1,26)  },
			};
		}
	}
}
