namespace Application.Abstraction
{
	public interface IHolidayService<T> where T : class
	{
		IList<T> GetHolidayEntries(int year);
	}
}
	