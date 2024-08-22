using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Request
{
	public class DatePeriodWithPublicHolidayRequest : DatePeriodRequest
	{
		/// <summary>
		/// Public Holiday Date list in string (should be in yyyy-mm-dd)
		/// </summary>
		[Required]
		[FromQuery]
		public IEnumerable<string> PublicHolidayDates { get; set; }
	}
}
