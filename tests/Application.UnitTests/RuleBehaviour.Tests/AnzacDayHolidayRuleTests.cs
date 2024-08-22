using Application.RuleBehaviour;
using Domain.Common;
using Domain.Enum;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class AnzacDayHolidayRuleTests
	{
		private Mock<ILogger<AnzacDayHolidayRule>> _mockLogger;

		[Test]
		public void ProcessRule_NotAnzacDay()
		{
			_mockLogger = new Mock<ILogger<AnzacDayHolidayRule>>();
			var testObject = new AnzacDayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = "NSW"
			}), _mockLogger.Object);

			Assert.AreEqual(testObject.ProcessRule(new DateTime(2019, 1, 21)), false);
		}

		[TestCaseSource(nameof(AnzacDayTestCases))]
		public void ProcessRule_AnzacDay(DateTime dateTime, States state, bool expected)
		{
			_mockLogger = new Mock<ILogger<AnzacDayHolidayRule>>();
			var testObject = new AnzacDayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = state.ToString()
			}), _mockLogger.Object);
			
			Assert.AreEqual(testObject.ProcessRule(dateTime), expected);
		}

		public static object[] AnzacDayTestCases =
		{
			new object[] { new DateTime(2021,4,27), States.ACT, false },
			new object[] { new DateTime(2021,4,26), States.SA, true },
			new object[] { new DateTime(2022,4,25), States.NT, true },
			new object[] { new DateTime(2021,4,25), States.WA, false },
			new object[] { new DateTime(2022,4,25), States.NSW, true }
		};
	}
}
