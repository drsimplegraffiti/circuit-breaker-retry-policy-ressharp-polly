using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FormulaOne.Api.Controllers;
using FormulaOne.Api.Services;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static FormulaOne.AirlineService.SwaggerIgnoreFilter;

namespace FormulaOne.Api.Controllers
{
    public class FlightController : BaseController
    {
        private readonly IFlightService _flightService;
        public FlightController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IMapper mapper, IFlightService flightService) : base(logger, unitOfWork, mapper)
        {
            _flightService = flightService;
        }

        [HttpGet]
      
        public async Task<IActionResult> GetFlights()
        {
           try
           {
                var result = await _flightService.GetAllAvailableFlights();
                return Ok(result);
           }
           catch (Exception e)
           {
                throw new Exception(e.Message);
           }
        }


    }
}