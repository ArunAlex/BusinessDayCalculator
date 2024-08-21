using Application.RuleBehaviour;
using Application.UseCase;
using Domain.Common;
using Domain.Enum;
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
	public class WeekdayHolidayRuleTests
	{
		private WeekdayHolidayRule testObject;

		[SetUp]
		public void Setup()
		{
			testObject = new WeekdayHolidayRule();
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
