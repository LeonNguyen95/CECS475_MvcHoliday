using MvcHoliday.Models;

using System.Text.Json;

namespace MvcHoliday.Services
{
    public class HolidaysApiService : IHolidaysApiService
    {
        private static readonly HttpClient client;

        static HolidaysApiService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://date.nager.at")
            };
        }

        public async Task<List<HolidayModel>> GetHolidays(string countryCode, int year)
        {
            // Build the Url of Nager.Date API using the `year` and the `countryCode` parameters
            var url = string.Format("/api/v2/PublicHolidays/{0}/{1}", year, countryCode);
            var result = new List<HolidayModel>();

            // Make an API call using GetAsync() that send a GET request to the specified Uri as an asynchronous operation
            // The method returns System.Net.Http.HttpResponseMessage object that represents an HTTP response message including
            // the state code and data
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Call the ReadAsStringAsync() that serializes the HTTP content to a string
                var stringResponse = await response.Content.ReadAsStringAsync();

                // Using JasonSerializer to Deserialize the JSON response string in a List of HolidayModel objects
                result = JsonSerializer.Deserialize<List<HolidayModel>>(stringResponse,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
