using Domain.Common.Request;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet("Weekdays")]
		public IActionResult GetWeekdays([FromQuery] DatePeriodRequest request)
		{
			var days = _businessDayCounter.WeekdaysBetweenTwoDates(request.StartDate, request.EndDate);

			if (days < 0)
			{
				return new BadRequestObjectResult("Dates provided has exceeded the system datetime range");
			}

			return new OkObjectResult(days);
		}

		/// <summary>
		/// Get number of Working days in the given period and public holidays
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet("BusinessDays")]
		public IActionResult GetBusinessDays([FromQuery] DatePeriodWithPublicHolidayRequest request)
		{
			var publicHolidays = request.PublicHolidays ?? Enumerable.Empty<DateTime>();
			var businessDays = _businessDayCounter.BusinessDaysBetweenTwoDates(request.StartDate, request.EndDate, publicHolidays.ToList());

			if (businessDays < 0)
			{
				return new BadRequestObjectResult("Dates provided has exceeded the system datetime range");
			}

			return new OkObjectResult(businessDays);
		}

		/// <summary>
		///  Get number of Working days in the given period using Australia Public Holiday rules
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet("AUBusinessDays")]
		public IActionResult GetBusinessDaysByAURules([FromQuery] DatePeriodRequest request)
		{
			var businessDays = _businessDayCounter.BusinessDaysBetweenTwoDates(request.StartDate, request.EndDate, _rules.Where(x => x.CountryCode == "AU"));

			if (businessDays < 0)
			{
				return new BadRequestObjectResult("Dates provided has exceeded the system datetime range");
			}

			return new OkObjectResult(businessDays);
		}
	}
}
