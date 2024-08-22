using Domain.Common;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.RuleBehaviour
{
	/// <summary>
	/// Process date is Anzac day as per the state rule
	/// </summary>
	public class AnzacDayHolidayRule : IHolidayRule
	{
		private readonly string? _state;

		public AnzacDayHolidayRule(IOptions<PublicHolidayOptions> options)
		{
			_state = options.Value.State ?? "All";
		}

		public string CountryCode => "AU";

		public bool ProcessRule(DateTime dateTime)
		{
			var anzacDay = GetAnzacDay(dateTime.Year, Enum.Parse<States>(_state));
			return anzacDay.Date.Equals(dateTime.Date);
		}

		private DateTime GetAnzacDay(int year, States state)
		{
			if (state == States.ACT || state == States.NT || state == States.SA || state == States.WA)
			{
				var anzacDay = new DateTime(year, 4, 25);
				if (anzacDay.DayOfWeek == DayOfWeek.Sunday)
				{
					return anzacDay.AddDays(1);
				}

				if (anzacDay.DayOfWeek == DayOfWeek.Saturday)
				{
					return anzacDay.AddDays(2);
				}
				return anzacDay;
			}
			return new DateTime(year, 4, 25);
		}
	}
}
