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
	public class EasterHolidayRuleTests
	{
		private EasterHolidayRule testObject;

		[SetUp]
		public void Setup()
		{
			testObject = new EasterHolidayRule();
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
