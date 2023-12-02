
using FormulaOne.AirlineService.Services;
using Microsoft.AspNetCore.Mvc;
using static FormulaOne.AirlineService.SwaggerIgnoreFilter;

namespace FormulaOne.AirlineService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FlightsCalendarController: ControllerBase
    {
        private readonly ILogger<FlightsCalendarController> _logger;
        public readonly ICalenderService _calenderService;

        public FlightsCalendarController(ILogger<FlightsCalendarController> logger, ICalenderService calenderService)
        {
            _logger = logger;
            _calenderService = calenderService;
        }

        [HttpGet]
        [LayerTwo]
        public async Task<IActionResult> Index()
        {
            try
            {
                var flights = await _calenderService.GetAvailableFlights();
                return Ok(flights);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Service not available {ex}");
            }
        }
    }
}