namespace Application.Abstraction
{
	public interface IHolidayAdapter<T> where T : class
	{
		IList<T> GetHolidayEntries(int year);
	}
}
	