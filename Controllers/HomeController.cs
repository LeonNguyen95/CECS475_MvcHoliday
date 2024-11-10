using Microsoft.AspNetCore.Mvc;
using MvcHoliday.Models;
using MvcHoliday.Services;
using System.Diagnostics;

namespace MvcHoliday.Controllers
{
    public class HomeController : Controller
    {
        public readonly IHolidaysApiService _holidaysApiService;
        public HomeController(IHolidaysApiService holidaysApiService)
        {
            _holidaysApiService = holidaysApiService;
        }
        public async Task<IActionResult> Index(string countryCode, int year)
        {
            List<HolidayModel> holidays = new List<HolidayModel>();
            // Inject HolidaysApiService and call GetHolidays() by passing the countryCode and year parameters
            // we receive inside the Index action method.
            if (!string.IsNullOrEmpty(countryCode) && year > 0)
            {
                holidays = await _holidaysApiService.GetHolidays(countryCode, year);
            }
            else
            {
                holidays = await _holidaysApiService.GetHolidays("US", DateTime.Now.Year);
            }

            return View(holidays);
        }
    }
}
