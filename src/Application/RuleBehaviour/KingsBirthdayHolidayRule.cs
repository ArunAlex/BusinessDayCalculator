using Domain.Common;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.RuleBehaviour
{
	/// <summary>
	/// Process date is Kings Birthday day as per the state rule
	/// </summary>
	public class KingsBirthdayHolidayRule : IHolidayRule
	{
		private readonly string? _state;

		public string CountryCode => "AU";

		public KingsBirthdayHolidayRule(IOptions<PublicHolidayOptions> options)
		{
			_state = options.Value.State ?? "All";
		}

		public bool ProcessRule(DateTime dateTime)
		{
			var kingsBirthday = GetKingsBirthday(dateTime.Year, Enum.Parse<States>(_state));
			return !kingsBirthday.HasValue || kingsBirthday.Value.Date.Equals(dateTime.Date) ;
		}

		public DateTime? GetKingsBirthday(int year, States state)
		{
			DateTime? kingsBirthday;
			switch (state)
			{
				case States.All:
					kingsBirthday = null; break;
				case States.ACT:
				case States.NSW:
				case States.NT:
				case States.SA:
				case States.TAS:
				case States.VIC:
					//second Monday in June
					var dt = new DateTime(year, 6, 1);
					kingsBirthday = dt.FindNext(DayOfWeek.Monday).AddDays(7);
					break;

				case States.QLD:
					//first Monday in October
					if (year >= 2016 || year == 2012)
					{
						var dtQld1 = new DateTime(year, 10, 1);
						kingsBirthday = dtQld1.FindNext(DayOfWeek.Monday);
					}
					else
					{
						//before 2016 was in June
						var dtQld2 = new DateTime(year, 6, 1);
						kingsBirthday = dtQld2.FindNext(DayOfWeek.Monday).AddDays(7);
					}
					break;

				case States.WA:
					//last Monday of September or first of October. No firm rule, all recent dates are September
					var dtWA = new DateTime(year, 9, 30);
					kingsBirthday = dtWA.FindPrevious(DayOfWeek.Monday);
					break;

				default:
					kingsBirthday = null; break;
			}

			return kingsBirthday;
		}
	}
}
