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
	public class DatePeriodRequest
	{
		/// <summary>
		/// Start Date in yyyy-mm-dd
		/// </summary>
		[Required]
		[FromQuery]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[RegularExpression(@"(\d{4})-(\d{2})-(\d{2})", ErrorMessage = "Should be in yyyy-mm-dd format")]
		public string StartDate { get; set; }

		/// <summary>
		/// End Date in yyyy-mm-dd
		/// </summary>
		[Required]
		[FromQuery]
		[RegularExpression(@"(\d{4})-(\d{2})-(\d{2})", ErrorMessage = "Should be in yyyy-mm-dd format")]
		public string EndDate { get; set; }
	}
}
