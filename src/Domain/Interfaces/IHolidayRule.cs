namespace Domain.Interfaces
{
	public interface IHolidayRule
	{
		string CountryCode { get; }

		bool ProcessRule(DateTime dateTime);
	}
}
