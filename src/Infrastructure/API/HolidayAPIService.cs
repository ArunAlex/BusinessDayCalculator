using Application.Abstraction;
using Domain.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace Infrastructure.API
{
    /// <summary>
    /// This Service to return a list of holidays in AU
    /// This is not part of the task excerise but was trying a different approach 
    /// to get a list of holidays.
    /// </summary>
	public class HolidayAPIService : IHolidayAdapter<Holiday>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _httpClientFactory;

        public HolidayAPIService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
        }

		public IList<Holiday> GetHolidayEntries(int year)
        {
            return (_memoryCache.GetOrCreate(
                     $"AU_{year}", _ => GetData(year)))!;
        }

		private IList<Holiday> GetData(int year)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var response =
                httpClient.GetFromJsonAsync<IList<Holiday>>(
                    $"https://date.nager.at/api/v3/publicholidays/{year}/AU").Result;

            return response!;
        }
    }
}
