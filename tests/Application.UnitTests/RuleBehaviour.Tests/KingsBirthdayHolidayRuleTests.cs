using Application.RuleBehaviour;
using Domain.Common;
using Domain.Enum;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class KingsBirthdayHolidayRuleTests
	{
		private Mock<ILogger<KingsBirthdayHolidayRule>> _mockLogger;

		[TestCaseSource(nameof(KingsBirthdayTestCases))]
		public void ProcessRule_KingsBirthday(DateTime dateTime, States state, bool expected)
		{
			_mockLogger = new Mock<ILogger<KingsBirthdayHolidayRule>>();
			var testObject = new KingsBirthdayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = state.ToString()
			}), _mockLogger.Object);

			Assert.AreEqual(testObject.ProcessRule(dateTime), expected);
		}

		public static object[] KingsBirthdayTestCases =
		{
			new object[] { new DateTime(2024,10,7), States.QLD, true },
			new object[] { new DateTime(2015,6,8), States.QLD, true },
			new object[] { new DateTime(2016,6,9), States.QLD, false },
			new object[] { new DateTime(2021,4,26), States.SA, false },
			new object[] { new DateTime(2022,6,3), States.NT, false },
			new object[] { new DateTime(2021,9,27), States.WA, true },
			new object[] { new DateTime(2022,6,13), States.NSW, true }
		};
	}
}
