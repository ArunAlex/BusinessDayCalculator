using Domain.Common;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.RuleBehaviour
{
	public class LabourDayHolidayRule : IHolidayRule
	{
		private readonly string? _state;

		public string CountryCode => "AU";

		public LabourDayHolidayRule(IOptions<PublicHolidayOptions> options)
		{
			_state = options.Value.State ?? "All";
		}

		public bool ProcessRule(DateTime dateTime)
		{
			var labourDay = GetLabourDay(dateTime.Year, Enum.Parse<States>(_state));
			return !labourDay.HasValue || labourDay.Value.Date.Equals(dateTime.Date);
		}

		private DateTime? GetLabourDay(int year, States state)
		{
			DateTime? labourDay = null;
			switch (state)
			{
				case States.All:
					 break;
				case States.ACT:
				case States.NSW:
				case States.SA:
					//Australian Capital Territory, New South Wales and South Australia = first Monday in October
					labourDay = FindNext(new DateTime(year, 10, 1), DayOfWeek.Monday); break;

				case States.NT:
				case States.QLD:
					//Northern Territory and Queensland = May Day
					labourDay = FindNext(new DateTime(year, 5, 1), DayOfWeek.Monday); break;

				case States.TAS:
				case States.VIC:
					//Victoria and Tasmania = second Monday in March ("Eight Hours Day").
					labourDay = FindNext(new DateTime(year, 3, 1), DayOfWeek.Monday).AddDays(7); break;

				case States.WA:
					//Western Australia= first Monday in March
					labourDay = FindNext(new DateTime(year, 3, 1), DayOfWeek.Monday); break;

				default:
					break;
			}

			return labourDay;
		}

		public DateTime FindNext(DateTime hol, DayOfWeek day)
		{
			while (hol.DayOfWeek != day)
			{
				hol = hol.AddDays(1);
			}

			return hol;
		}
	}
}
