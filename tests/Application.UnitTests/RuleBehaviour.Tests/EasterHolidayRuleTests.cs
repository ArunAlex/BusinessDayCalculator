using Application.RuleBehaviour;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class EasterHolidayRuleTests
	{
		private EasterHolidayRule testObject;
		private Mock<ILogger<EasterHolidayRule>> _mockLogger;

		[SetUp]
		public void Setup()
		{
			_mockLogger = new Mock<ILogger<EasterHolidayRule>>();
			testObject = new EasterHolidayRule(_mockLogger.Object);
		}

		[Test]
		public void ProcessRule_NotEaster()
		{
			Assert.AreEqual(testObject.ProcessRule(new DateTime(2024, 4, 2)), false);
		}

		[Test]
		public void ProcessRule_GoodFriday()
		{
			Assert.AreEqual(testObject.ProcessRule(new DateTime(2024, 3, 29)), true);
		}

		[Test]
		public void ProcessRule_EasterMonday()
		{
			Assert.AreEqual(testObject.ProcessRule(new DateTime(2024, 4, 1)), true);
		}
	}
}
