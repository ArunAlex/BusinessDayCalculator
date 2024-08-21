using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Request
{
	public class DatePeriodRequest
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
