using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
	[TestFixture]
	public class BusinessDayCounterTests
	{
		private BusinessDayCounter testObject;

		[SetUp]
		public void Setup()
		{
			testObject = new BusinessDayCounter();
		}

		[TestCaseSource(nameof(WeekdayTestCases))]
		public void GetWeekdays(DateTime firstDate, DateTime secondDate, int expectedValue)
		{
			Assert.AreEqual(testObject.WeekdaysBetweenTwoDates(firstDate, secondDate), expectedValue);
		}

		[TestCaseSource(nameof(BusinessDayTestCases))]
		public void GetBusinessDays(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays, int expectedValue)
		{
			Assert.AreEqual(testObject.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays), expectedValue);
		}

		public static object[] WeekdayTestCases =
		{
			new object[] { new DateTime(2013,10,7), new DateTime(2013,10,9), 1 },
			new object[] { new DateTime(2013,10,5), new DateTime(2013,10,14), 5 },
			new object[] { new DateTime(2013,10,7), new DateTime(2014,1,1), 61 },
			new object[] { new DateTime(2013,10,7), new DateTime(2013,10,5), 0 }
		};

		public static object[] BusinessDayTestCases =
		{
			new object[] {
				new DateTime(2013,10,7),
				new DateTime(2013,10,9),
				new List<DateTime>() { new DateTime(2013, 12,25), new DateTime(2013,12,26), new DateTime(2014,1,1) },
				1 
			},
			new object[] {
				new DateTime(2013,12,24),
				new DateTime(2013,10,27),
				new List<DateTime>() { new DateTime(2013, 12,25), new DateTime(2013,12,26), new DateTime(2014,1,1) },
				0 
			},
			new object[] { 
				new DateTime(2013,10,7),
				new DateTime(2014,1,1),
				new List<DateTime>() { new DateTime(2013, 12,25), new DateTime(2013,12,26), new DateTime(2014,1,1) },
				59 
			},
		};
	}
}
