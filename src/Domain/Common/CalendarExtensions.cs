using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
	public static class CalendarExtensions
	{
		public static DateTime FindNext(this DateTime dt, DayOfWeek day)
		{
			while (dt.DayOfWeek != day)
			{
				dt = dt.AddDays(1);
			}

			return dt;
		}

		public static DateTime FindPrevious(this DateTime dt, DayOfWeek day)
		{
			while (dt.DayOfWeek != day)
			{
				dt = dt.AddDays(-1);
			}

			return dt;
		}

		public static DateTime NextWeekend(this DateTime dt)
		{
			if (dt.DayOfWeek == DayOfWeek.Sunday)
			{
				dt = dt.AddDays(1);
			}
			else if (dt.DayOfWeek == DayOfWeek.Saturday)
			{
				dt = dt.AddDays(2);
			}
			return dt;
		}
	}
}
