using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCase;
using Domain.Interfaces;
using Moq;

namespace Application.UnitTests.UseCase.Tests
{
    [TestFixture]
    public class BusinessDayCounterTests
    {
        private BusinessDayCounter testObject;
        private Mock<IHolidayRule> mockHolidayRule1;
        private Mock<IHolidayRule> mockHolidayRule2;
        private Mock<IHolidayRule> mockHolidayRule3;

        [SetUp]
        public void Setup()
        {
            testObject = new BusinessDayCounter();
			mockHolidayRule1 = new Mock<IHolidayRule>();
			mockHolidayRule1.Setup(x => x.ProcessRule(It.IsAny<DateTime>())).Returns(false);
			mockHolidayRule2 = new Mock<IHolidayRule>();
			mockHolidayRule2.Setup(x => x.ProcessRule(It.IsAny<DateTime>())).Returns(false);
			mockHolidayRule3 = new Mock<IHolidayRule>();
			mockHolidayRule3.Setup(x => x.ProcessRule(It.IsAny<DateTime>())).Returns(true);
		}

        [TestCaseSource(nameof(WeekdayTestCases))]
        public void GetWeekdays(DateTime firstDate, DateTime secondDate, int expectedValue)
        {
            Assert.AreEqual(testObject.WeekdaysBetweenTwoDates(firstDate, secondDate), expectedValue);
        }

        [TestCaseSource(nameof(BusinessDayTestCases))]
        public void GetBusinessDays(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays, int expectedValue)
        {
            Assert.AreEqual(testObject.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays), expectedValue);
        }

        [Test]
        public void GetBusinessDays_SingleHolidayRule()
        {
            Assert.AreEqual(testObject.BusinessDaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), new List<IHolidayRule>() { mockHolidayRule1.Object }), 1);
        }

        [Test]
        public void GetBusinessDays_MultipleHolidayRules()
        {
            Assert.AreEqual(testObject.BusinessDaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9),
                new List<IHolidayRule>() {
                    mockHolidayRule1.Object ,
                    mockHolidayRule2.Object,
                    mockHolidayRule3.Object
                }), 0);
        }

        public static object[] WeekdayTestCases =
        {
            new object[] { new DateTime(2013,10,7), new DateTime(2013,10,9), 1 },
            new object[] { new DateTime(2013,10,5), new DateTime(2013,10,14), 5 },
            new object[] { new DateTime(2013,10,7), new DateTime(2014,1,1), 61 },
            new object[] { new DateTime(2013,10,7), new DateTime(2013,10,5), 0 }
        };

        public static object[] BusinessDayTestCases =
        {
            new object[] {
                new DateTime(2013,10,7),
                new DateTime(2013,10,9),
                new List<DateTime>() { new DateTime(2013, 12,25), new DateTime(2013,12,26), new DateTime(2014,1,1) },
                1
            },
            new object[] {
                new DateTime(2013,12,24),
                new DateTime(2013,10,27),
                new List<DateTime>() { new DateTime(2013, 12,25), new DateTime(2013,12,26), new DateTime(2014,1,1) },
                0
            },
            new object[] {
                new DateTime(2013,10,7),
                new DateTime(2014,1,1),
                new List<DateTime>() { new DateTime(2013, 12,25), new DateTime(2013,12,26), new DateTime(2014,1,1) },
                59
            },
        };
    }
}
