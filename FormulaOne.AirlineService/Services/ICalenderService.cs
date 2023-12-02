

using FormulaOne.Entities.Dtos.Responses;

namespace FormulaOne.AirlineService.Services
{
    public interface ICalenderService
    {
        public Task<List<FlightDto>> GetAvailableFlights();
    }
}