using Application.RuleBehaviour;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class ChristmasHolidayRuleTests
	{
		private ChristmasHolidayRule testObject;
		private Mock<ILogger<ChristmasHolidayRule>> _mockLogger;

		[SetUp]
		public void Setup()
		{
			_mockLogger = new Mock<ILogger<ChristmasHolidayRule>>();
			testObject = new ChristmasHolidayRule(_mockLogger.Object);
		}

		[Test]
		public void ProcessRule_NotChristmas()
		{
			Assert.AreEqual(testObject.ProcessRule(new DateTime(2024, 4, 2)), false);
		}

		[Test]
		public void ProcessRule_Christmas()
		{
			Assert.AreEqual(testObject.ProcessRule(new DateTime(2021, 12, 27)), true);
		}

		[Test]
		public void ProcessRule_BoxingDay()
		{
			Assert.AreEqual(testObject.ProcessRule(new DateTime(2021, 12, 28)), true);
		}
	}
}
