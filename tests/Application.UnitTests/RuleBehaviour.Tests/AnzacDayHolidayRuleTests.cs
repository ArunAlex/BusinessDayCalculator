using Application.RuleBehaviour;
using Application.UseCase;
using Domain.Common;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.RuleBehaviour.Tests
{
	[TestFixture]
	public class AnzacDayHolidayRuleTests
	{
		[Test]
		public void ProcessRule_NotAnzacDay()
		{
			var testObject = new AnzacDayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = "NSW"
			}));

			Assert.AreEqual(testObject.ProcessRule(new DateTime(2019, 1, 21)), false);
		}

		[TestCaseSource(nameof(AnzacDayTestCases))]
		public void ProcessRule_AnzacDay(DateTime dateTime, States state, bool expected)
		{
			var testObject = new AnzacDayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = state.ToString()
			}));
			
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
