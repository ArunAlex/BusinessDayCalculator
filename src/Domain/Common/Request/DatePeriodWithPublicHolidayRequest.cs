using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Request
{
	public class DatePeriodWithPublicHolidayRequest : DatePeriodRequest
	{
		public DateTime[]? PublicHolidays { get; set; }
	}
}
