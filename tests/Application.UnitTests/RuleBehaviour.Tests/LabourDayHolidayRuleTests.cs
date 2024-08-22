using Application.RuleBehaviour;
using Domain.Common;
using Domain.Enum;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class LabourDayHolidayRuleTests
	{
		private Mock<ILogger<LabourDayHolidayRule>> _mockLogger;

		[TestCaseSource(nameof(LabourDayTestCases))]
		public void ProcessRule_LabourDay(DateTime dateTime, States state, bool expected)
		{
			_mockLogger = new Mock<ILogger<LabourDayHolidayRule>>();
			var testObject = new LabourDayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = state.ToString()
			}), _mockLogger.Object);

			Assert.AreEqual(testObject.ProcessRule(dateTime), expected);
		}

		public static object[] LabourDayTestCases =
		{
			new object[] { new DateTime(2024,5,6), States.QLD, true },
			new object[] { new DateTime(2016,6,9), States.QLD, false },
			new object[] { new DateTime(2021,1,10), States.SA, false },
			new object[] { new DateTime(2022,6,3), States.NT, false },
			new object[] { new DateTime(2022,3,14), States.VIC, true },
			new object[] { new DateTime(2022,3,6), States.TAS, false },
			new object[] { new DateTime(2021,3,1), States.WA, true },
			new object[] { new DateTime(2021,3,2), States.WA, false },
			new object[] { new DateTime(2022,10,3), States.NSW, true }
		};
	}
}
