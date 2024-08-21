using Application.RuleBehaviour;
using Application.UseCase;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class WeekendHolidayRuleTests
	{
		private WeekendHolidayRule testObject;

		[SetUp]
		public void Setup()
		{
			testObject = new WeekendHolidayRule();
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
