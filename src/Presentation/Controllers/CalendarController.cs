using Domain.Common.Request;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
	[Produces("application/json")]
	[ApiController]
	[Route("[controller]")]
	public class CalendarController : ControllerBase
	{
		private readonly ILogger<CalendarController> _logger;
		private readonly IBusinessDayCounter _businessDayCounter;
		private readonly IEnumerable<IHolidayRule> _rules;

		public CalendarController(IBusinessDayCounter businessDayCounter, IEnumerable<IHolidayRule> rules, ILogger<CalendarController> logger)
		{
			_logger = logger;
			_rules = rules;
			_businessDayCounter = businessDayCounter;
		}

		/// <summary>
		/// Get number of Week days in the given period
		/// </summary>
		/// <param name="request"></param >
		/// <response code="200">Success</response>
		/// <response code="400">Date Time provided is invalid</response>
		[HttpGet("Weekdays")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<int> GetWeekdays([FromQuery] DatePeriodRequest request)
		{
			if (!DateTime.TryParseExact(request.StartDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime startDate))
			{
				return new BadRequestObjectResult("Start Date is not in yyyy-mm-dd format");
			}

			if (!DateTime.TryParseExact(request.EndDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime endDate))
			{
				return new BadRequestObjectResult("End Date is not in yyyy-mm-dd format");
			}

			var days = _businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

			return new OkObjectResult(days);
		}

		/// <summary>
		/// Get number of Working days in the given period and public holidays
		/// </summary>
		/// <param name="request"></param>
		/// <response code="200">Success</response>
		/// <response code="400">Date Time provided is invalid</response>
		[HttpGet("BusinessDays")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<int> GetBusinessDays([FromQuery] DatePeriodWithPublicHolidayRequest request)
		{
			if (!DateTime.TryParseExact(request.StartDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime startDate))
			{
				return new BadRequestObjectResult("Start Date is not in yyyy-mm-dd format");
			}

			if (!DateTime.TryParseExact(request.EndDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime endDate))
			{
				return new BadRequestObjectResult("End Date is not in yyyy-mm-dd format");
			}

			var publicHolidays = request.PublicHolidayDates.ToList().Select((x) =>
			{
				if (DateTime.TryParseExact(x, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime pbDate))
				{
					return pbDate;
				}
				return (DateTime?)null;
			});

			if (publicHolidays.Any(x => !x.HasValue))
			{
				return new BadRequestObjectResult("All/Some of the public holiday dates are not in yyyy-mm-dd format");
			}

			var businessDays = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, publicHolidays.Select(x => x.Value).ToList());

			return new OkObjectResult(businessDays);
		}

		/// <summary>
		///  Get number of Working days in the given period using Australia Public Holiday rules
		///  Rules include Holidays on the same weekday and on weekend which is new years and Australia day
		///  Special rules such as Christmas holiday, Easter Holiday, Kings Birthday, Labour Day, Anzac Day as per the state.
		/// </summary>
		/// <param name="request"></param>
		/// <response code="200">Success</response>
		/// <response code="400">Date Time provided is invalid</response>
		[HttpGet("AUBusinessDays")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<int> GetBusinessDaysByAURules([FromQuery] DatePeriodRequest request)
		{
			if (!DateTime.TryParseExact(request.StartDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime startDate))
			{
				return new BadRequestObjectResult("Start Date is not in yyyy-mm-dd format");
			}

			if (!DateTime.TryParseExact(request.EndDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime endDate))
			{
				return new BadRequestObjectResult("End Date is not in yyyy-mm-dd format");
			}

			var businessDays = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, _rules.Where(x => x.CountryCode == "AU"));

			return new OkObjectResult(businessDays);
		}
	}
}
