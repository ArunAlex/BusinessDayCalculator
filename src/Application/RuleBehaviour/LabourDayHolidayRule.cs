using Domain.Common;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.RuleBehaviour
{
	/// <summary>
	/// Process date is Labour day as per the state rule
	/// </summary>
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
					var dt = new DateTime(year, 10, 1);
					labourDay = dt.FindNext(DayOfWeek.Monday); break;

				case States.NT:
				case States.QLD:
					//Northern Territory and Queensland = May Day
					var dtNT = new DateTime(year, 5, 1);
					labourDay = dtNT.FindNext(DayOfWeek.Monday); break;

				case States.TAS:
				case States.VIC:
					//Victoria and Tasmania = second Monday in March ("Eight Hours Day").
					var dtTas = new DateTime(year, 3, 1);
					labourDay = dtTas.FindNext(DayOfWeek.Monday).AddDays(7); break;

				case States.WA:
					//Western Australia= first Monday in March
					var dtWA = new DateTime(year, 3, 1);
					labourDay = dtWA.FindNext(DayOfWeek.Monday); break;

				default:
					break;
			}

			return labourDay;
		}
	}
}
