using Application.RuleBehaviour;
using Application.UseCase;
using Domain.Common;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class ChristmasHolidayRuleTests
	{
		private ChristmasHolidayRule testObject;

		[SetUp]
		public void Setup()
		{
			testObject = new ChristmasHolidayRule();
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
