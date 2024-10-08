# BusinessDayCalculator

This is DesignCrowd technical challenge where the goal is to demonstrate:

* How you approach solution design;
* What production quality code means to you;
* Your ability to implement the correct solution to a spec;
* The tools, frameworks and language features you think are most suitable to use;


As a starting point, create a class that matches the following:
```
public class BusinessDayCounter
{
    public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
    {
      //todo
    }
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>
    publicHolidays)
    {
      //todo
    }
}
```

You will use this scaffold as the basis for building the solution to the following tasks.

## TASK 1

Calculates the number of weekdays in between two dates.
* Weekdays are Monday, Tuesday, Wednesday, Thursday, Friday.
* The returned count should not include either firstDate or secondDate - e.g. between Monday 07-Oct-2013 and Wednesday 09-Oct-2013 is one weekday.
* If secondDate is equal to or before firstDate, return 0.

## TASK 2

Calculate the number of business days in between two dates.
* Business days are Monday, Tuesday, Wednesday, Thursday, Friday, but excluding any dates which appear in the supplied list of public holidays.
* The returned count should not include either firstDate or secondDate - e.g. between Monday 07-Oct-2013 and Wednesday 09-Oct-2013 is one weekday.
* If secondDate is equal to or before firstDate, return 0.

## TASK 3

Design a data structure or hierarchy of structures which can define public holidays in a more complex fashion than simple dates.

This should cater for things such as:
* Public holidays which are always on the same day, e.g. Anzac Day on April 25th every year.
* Public holidays which are always on the same day, except when that falls on a weekend. e.g. New Year's Day on January 1st every year, unless that is a Saturday or Sunday, in which case the holiday is the next Monday.
* Public holidays on a certain occurrence of a certain day in a month. e.g. Queen's Birthday on the second Monday in June every year.
  
Given this data structure, the BusinessDaysBetweenTwoDates() function should be able to be extended to take a list of public holiday rules, rather than a list of DateTimes, and calculate the number of business days between two dates using those rules to define public holidays.

Approach: The Public Holidays are based in Australia and every state has a date for specific holidays such as Kings Birthday. Therefore the rules has been created based on those assumptions.

Every rule inherits the interface IHolidayRule. 
```
	public interface IHolidayRule
	{
		string CountryCode { get; }

		bool ProcessRule(DateTime dateTime);
	}
```

To explain how a rule behaves, for example KingsBirthdayHolidayRule inheriting it, will process where
+ state based public holiday date is returned and verified with date
+ checks for specific pattern eg NSW has the holiday every second monday of June or QLD has every first Monday of October.
  
These conditions are handled inside ProcessRule method. In addition, I have introduced country code for the rules in order to handle country holidays. Currently all the rules created has country code 'AU' and could be expanded to other countries for future purpose

All these rules will be passed as an IEnumerable below and these rules will be injected in the Startup.cs file. When we want to expand to other countries, we can change the injection rules based on the country code.
```
	int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IEnumerable<IHolidayRule> publicHolidays);
```

The solution is in .NET 7.0 and I have created a WEB API project where the functions are used inside the endpoints

* /Calendar/Weekdays - Gets No of Weekdays based on time period
* /Calendar/BusinessDays - Get No of Businessdays based on time period and public holiday date list
* /Calendar/AUBusinessDays - Get No of Businessdays that do not satisfy the public holiday rules in Australia

Note: I would like implement a Dockerfile so that we can run the api on a docker container but didnt have the time to do it. So please clone the project in Visual Studio and run the profile 'Core API' in launchSettings.json to view the swagger UI. You can test it in Swagger or in Postman using the app url https://localhost:7297 followed by the above endpoints



