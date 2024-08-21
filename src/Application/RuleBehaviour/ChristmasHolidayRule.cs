using Domain.Common;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.RuleBehaviour
{
	public class ChristmasHolidayRule : IHolidayRule
	{
		public string CountryCode => "AU";

		public ChristmasHolidayRule()
		{
		}

		public bool ProcessRule(DateTime dateTime)
		{
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
			return FixWeekend(new DateTime(year, 12, 25));
		}

		private DateTime GetBoxingDay(int year)
		{
			DateTime hol = new DateTime(year, 12, 26);
			//if Xmas=Sun, it's shifted to Mon and 26 also gets shifted
			bool isSundayOrMonday =
				hol.DayOfWeek == DayOfWeek.Sunday ||
				hol.DayOfWeek == DayOfWeek.Monday;
			hol = FixWeekend(hol);
			if (isSundayOrMonday)
			{
				hol = hol.AddDays(1);
			}
			return hol;
		}

		private DateTime FixWeekend(DateTime hol)
		{
			if (hol.DayOfWeek == DayOfWeek.Sunday)
			{
				hol = hol.AddDays(1);
			}
			else if (hol.DayOfWeek == DayOfWeek.Saturday)
			{
				hol = hol.AddDays(2);
			}
			return hol;
		}

	}
}
