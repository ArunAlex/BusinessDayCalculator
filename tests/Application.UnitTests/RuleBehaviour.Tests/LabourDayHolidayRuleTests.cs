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
	public class LabourDayHolidayRuleTests
	{

		[TestCaseSource(nameof(LabourDayTestCases))]
		public void ProcessRule_LabourDay(DateTime dateTime, States state, bool expected)
		{
			var testObject = new LabourDayHolidayRule(Options.Create(new PublicHolidayOptions
			{
				State = state.ToString()
			}));

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
