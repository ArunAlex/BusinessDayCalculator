using Application.Abstraction;
using Domain.Common;
using Domain.Interfaces;

namespace Application.RuleBehaviour
{
	/// <summary>
	/// Process date is if holiday on weekend (New years and Australia day) 
	/// </summary>
	public class WeekendHolidayRule : IHolidayRule
	{
		public WeekendHolidayRule() 
		{
		}

		public string CountryCode => "AU";

		public bool ProcessRule(DateTime date)
		{
			var holidays = GetDifferentDayHolidays(date.Year);

			var holidaysOnWeekends = holidays.Where(h => h.Date.DayOfWeek == DayOfWeek.Sunday || h.Date.DayOfWeek == DayOfWeek.Saturday);

			if (holidaysOnWeekends != null && holidaysOnWeekends.Count() > 0)
			{
				var holidaysNextMonday = holidaysOnWeekends.Select(x => new Holiday()
				{
					Date = x.Date.DayOfWeek == DayOfWeek.Sunday ? x.Date.AddDays(1) : x.Date.AddDays(2),
					Name = x.Name	
				});

				return holidaysNextMonday.Any(x => x.Date.Date == date.Date);
			}

			return false;
		}

		private List<Holiday> GetDifferentDayHolidays(int year)
		{
			return new List<Holiday>()
			{
				new Holiday() { Name= "New Year", Date = new DateTime(year, 1,1)  },
				new Holiday() { Name= "Australia Day", Date = new DateTime(year, 1,26)  },
			};
		}
	}
}
