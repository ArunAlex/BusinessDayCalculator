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
	public class KingsBirthdayHolidayRuleTests
	{
		[TestCaseSource(nameof(KingsBirthdayTestCases))]
		public void ProcessRule_KingsBirthday(DateTime dateTime, States state, bool expected)
		{
			var testObject = new KingsBirthdayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = state.ToString()
			}));

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
