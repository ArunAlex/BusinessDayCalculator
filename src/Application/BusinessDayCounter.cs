using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public class BusinessDayCounter
	{
		public BusinessDayCounter()
		{
		}

		/// <summary>
		/// Task 1: Get Number of Weekdays between two  dates
		/// </summary>
		/// <param name="firstDate"></param>
		/// <param name="secondDate"></param>
		/// <returns></returns>
		public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
		{
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
	}
}
