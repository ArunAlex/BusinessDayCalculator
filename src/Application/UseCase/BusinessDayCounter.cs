using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCase
{
	public class BusinessDayCounter : IBusinessDayCounter
	{
		private readonly ILogger<BusinessDayCounter> _logger;

		public BusinessDayCounter(ILogger<BusinessDayCounter> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Task 1: Get Number of Weekdays between two  dates
		/// </summary>
		/// <param name="firstDate"></param>
		/// <param name="secondDate"></param>
		/// <returns></returns>
		public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
		{
			_logger.LogInformation("Call WeekdaysBetweenTwoDates");
			var firstDay = firstDate.Date;
			var endDay = secondDate.Date;

			if (firstDay >= endDay)
			{
				return 0;
			}

			var counter = 0;
			var start = firstDay.AddDays(1);
			while (start < endDay)
			{
				if (!(start.DayOfWeek.Equals(DayOfWeek.Saturday) || start.DayOfWeek.Equals(DayOfWeek.Sunday)))
				{
					counter++;
				}

				start = start.AddDays(1);
			}

			return counter;
		}

		/// <summary>
		/// Task 2: Get Number of Business Days between Two dates but excluding Public holidays 
		/// </summary>
		/// <param name="firstDate"></param>
		/// <param name="secondDate"></param>
		/// <param name="publicHolidays"></param>
		/// <returns></returns>
		public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
		{
			_logger.LogInformation("Call BusinessDaysBetweenTwoDates using holidays");
			var firstDay = firstDate.Date;
			var endDay = secondDate.Date;

			if (firstDay >= endDay)
			{
				return 0;
			}

			var counter = 0;
			var start = firstDay.AddDays(1);
			while (start < endDay)
			{
				if (!publicHolidays.Contains(start) &&
					!(start.DayOfWeek.Equals(DayOfWeek.Saturday) || start.DayOfWeek.Equals(DayOfWeek.Sunday)))
				{
					counter++;
				}

				start = start.AddDays(1);
			}

			return counter;
		}

		/// <summary>
		/// Task 3: Get Number of Business Days between Two dates by rules
		/// </summary>
		/// <param name="firstDate"></param>
		/// <param name="secondDate"></param>
		/// <param name="publicHolidayRules"></param>
		/// <returns></returns>
		public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IEnumerable<IHolidayRule> publicHolidayRules)
		{
			_logger.LogInformation("Call BusinessDaysBetweenTwoDates using rules");
			var firstDay = firstDate.Date;
			var endDay = secondDate.Date;

			var counter = 0;
			var start = firstDay.AddDays(1);
			while (start < endDay)
			{
				if (!(start.DayOfWeek.Equals(DayOfWeek.Saturday) || start.DayOfWeek.Equals(DayOfWeek.Sunday)))
				{
					if (publicHolidayRules.All(x => !x.ProcessRule(start)))
					{
						counter++;
					}
				}

				start = start.AddDays(1);
			}

			return counter;
		}
	}
}
