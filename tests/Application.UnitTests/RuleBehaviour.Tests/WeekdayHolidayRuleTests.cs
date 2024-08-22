using Application.RuleBehaviour;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class WeekdayHolidayRuleTests
	{
		private WeekdayHolidayRule testObject;
		private Mock<ILogger<WeekdayHolidayRule>> _mockLogger;

		[SetUp]
		public void Setup()
		{
			_mockLogger = new Mock<ILogger<WeekdayHolidayRule>>();
			testObject = new WeekdayHolidayRule(_mockLogger.Object);
		}

		[TestCaseSource(nameof(WeekdayTestCases))]
		public void ProcessRule_WeekDay(DateTime dateTime, bool expected)
		{
			Assert.AreEqual(testObject.ProcessRule(dateTime), expected);
		}

		public static object[] WeekdayTestCases =
		{
			new object[] { new DateTime(2024,1,26), true },
			new object[] { new DateTime(2016,6,9), false },
			new object[] { new DateTime(2021,1,10), false },
			new object[] { new DateTime(2022,3,6),  false },
			new object[] { new DateTime(2022,10,2), false }
		};
	}
}
