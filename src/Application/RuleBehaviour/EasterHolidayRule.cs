using Domain.Common;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.RuleBehaviour
{
	public class EasterHolidayRule : IHolidayRule
	{
		private readonly string? _state;

		public string CountryCode => "AU";

		public EasterHolidayRule()
		{
		}

		public bool ProcessRule(DateTime dateTime)
		{
			return IsGoodFriday(dateTime) || IsEasterMonday(dateTime);
		}

		private bool IsGoodFriday(DateTime dateTime)
		{
			var easterSunday = GetEasterSunday(dateTime.Year);
			var goodFriday = easterSunday.Date.AddDays(-2);
			return goodFriday.Date.Equals(dateTime.Date);
		}

		private bool IsEasterMonday(DateTime dateTime)
		{
			var easterSunday = GetEasterSunday(dateTime.Year);
			var easterMonday = easterSunday.Date.AddDays(1);
			return easterMonday.Date.Equals(dateTime.Date);
		}

		private DateTime GetEasterSunday(int year)
		{
			//Oudin's Algorithm
			var a = year % 19;
			var b = year / 100;
			var c = (b - b / 4 - (8 * b + 13) / 25 + 19 * a + 15) % 30;
			var d = c - (c / 28) * (1 - (c / 28) * (29 / (c + 1)) * ((21 - a) / 11));
			var e = (year + year / 4 + d + 2 - b + b / 4) % 7;
			var f = d - e;

			var easterDay = 1 + (f + 27 + (f + 6) / 40) % 31;
			var easterMonth = 3 + (f + 26) / 30;

			return new DateTime(year, easterMonth, easterDay);
		}
	}
}
