using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{

    public class DriversController : BaseController
    {
        public DriversController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper)
        {
        }


        [HttpGet]
        [Route("{driverId:guid}")]
        public async Task<IActionResult> GetDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);
            if (driver == null)
            {
                return NotFound("Driver not found");
            }
            var result = _mapper.Map<GetDriverResponse>(driver);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _mapper.Map<Driver>(driver);
            await _unitOfWork.Drivers.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriver), new { driverId = result.Id }, result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateDriver( [FromBody] UpdateDriverRequest driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _mapper.Map<Driver>(driver);

            await _unitOfWork.Drivers.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _unitOfWork.Drivers.All();
            if (drivers == null)
            {
                return NotFound("No drivers found");
            }
            return Ok(_mapper.Map<IEnumerable<GetDriverResponse>>(drivers));
        }

        [HttpDelete]
        [Route("{driverId:guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);
            if (driver == null)
            {
                return NotFound("Driver not found");
            }
            await _unitOfWork.Drivers.Delete(driverId);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}