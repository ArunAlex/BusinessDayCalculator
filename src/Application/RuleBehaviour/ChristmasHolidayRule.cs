using Domain.Common;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.RuleBehaviour
{
	/// <summary>
	/// Process date is in Boxing day or Xmas day
	/// </summary>
	public class ChristmasHolidayRule : IHolidayRule
	{
		public string CountryCode => "AU";
		private readonly ILogger<ChristmasHolidayRule> _logger;

		public ChristmasHolidayRule(ILogger<ChristmasHolidayRule> logger)
		{
			_logger = logger;
		}

		public bool ProcessRule(DateTime dateTime)
		{
			_logger.LogInformation($"Invoke Christmas rule {dateTime.ToString("dd/MM/yyyy")}");
			return IsChristmas(dateTime) || IsBoxingDay(dateTime);
		}

		private bool IsChristmas(DateTime dateTime)
		{
			var christmasDay = GetChristmas(dateTime.Year);
			return christmasDay.Date.Equals(dateTime.Date);
		}

		private bool IsBoxingDay(DateTime dateTime)
		{
			var boxingDay = GetBoxingDay(dateTime.Year);
			return boxingDay.Date.Equals(dateTime.Date);
		}

		private DateTime GetChristmas(int year)
		{
			var dt = new DateTime(year, 12, 25);
			return dt.NextWeekend();
		}

		private DateTime GetBoxingDay(int year)
		{
			DateTime dt = new DateTime(year, 12, 26);

			//When Xmas is on Sun,holiday is shifted to Mon and Boxing day will also gets shifted by another day
			bool isSundayOrMonday = dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Monday;

			dt = dt.NextWeekend();
			if (isSundayOrMonday)
			{
				dt = dt.AddDays(1);
			}

			return dt;
		}
	}
}
