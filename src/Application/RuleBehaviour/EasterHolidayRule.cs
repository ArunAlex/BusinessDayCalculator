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
			//Oudin's Algorithm - http://www.smart.net/~mmontes/oudin.html

			var g = year % 19;
			var c = year / 100;
			var h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
			var i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));
			var j = (year + year / 4 + i + 2 - c + c / 4) % 7;
			var p = i - j;
			var easterDay = 1 + (p + 27 + (p + 6) / 40) % 31;
			var easterMonth = 3 + (p + 26) / 30;

			return new DateTime(year, easterMonth, easterDay);
		}
	}
}
