using Application.RuleBehaviour;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class WeekendHolidayRuleTests
	{
		private WeekendHolidayRule testObject;
		private Mock<ILogger<WeekendHolidayRule>> _mockLogger;

		[SetUp]
		public void Setup()
		{
			_mockLogger = new Mock<ILogger<WeekendHolidayRule>>();
			testObject = new WeekendHolidayRule(_mockLogger.Object);
		}

		[TestCaseSource(nameof(WeekendTestCases))]
		public void ProcessRule_Weekend(DateTime dateTime, bool expected)
		{
			Assert.AreEqual(testObject.ProcessRule(dateTime), expected);
		}

		public static object[] WeekendTestCases =
		{
			new object[] { new DateTime(2020,1,27), true },
			new object[] { new DateTime(2016,6,9), false },
			new object[] { new DateTime(2021,1,10), false },
			new object[] { new DateTime(2022,6,3), false },
			new object[] { new DateTime(2022,1,3), true },
		};
	}
}
