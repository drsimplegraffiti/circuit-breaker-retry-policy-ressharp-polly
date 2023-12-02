

using FormulaOne.Entities.Dtos.Responses;

namespace FormulaOne.AirlineService.Services
{
    public class CalenderService : ICalenderService
    {
        private DateTime _recoveryTime = DateTime.UtcNow;
        private static readonly Random Random = new();

        public Task<List<FlightDto>> GetAvailableFlights()
        {
            if (_recoveryTime > DateTime.UtcNow)
            {
                throw new Exception("Service not available");
            }

            if (_recoveryTime < DateTime.UtcNow && Random.Next(1, 4) == 1)
            {
                _recoveryTime = DateTime.UtcNow.AddSeconds(10);
            }

            var flights = new List<FlightDto>
            {
                new FlightDto
                {
                    Arrival = "London",
                    Departure = "Dubai",
                    Price = 10000,
                    FlightDate = DateTime.Now.AddDays(3)
                },
                new FlightDto
                {
                    Arrival = "USA",
                    Departure = "Lagos",
                    Price = 2500,
                    FlightDate = DateTime.Now.AddDays(3)
                },
                new FlightDto
                {
                    Arrival = "Germany",
                    Departure = "Ibadan",
                    Price = 4000,
                    FlightDate = DateTime.Now.AddDays(3)
                }
            };

            return Task.FromResult(flights);
        }
    }


}
