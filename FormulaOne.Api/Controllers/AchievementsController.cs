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
    public class AchievementsController : BaseController
    {
        public AchievementsController(ILogger<BaseController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{driverId:guid}")]
        public async Task<IActionResult> GetDriverAchievements(Guid driverId){
            var achievements = await _unitOfWork.Achievements.GetByDriverAchievementAsync(driverId);
            if(achievements == null){
                return NotFound("No achievements found for this driver");
            }
            var result = _mapper.Map<DriverAchievementResponse>(achievements);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievement){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var result = _mapper.Map<Achievement>(achievement);
            await _unitOfWork.Achievements.Add(result);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetDriverAchievements), new {driverId = result.DriverId}, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAchievement(Guid id, [FromBody] UpdateDriverAchievementRequest achievement){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var result = await _unitOfWork.Achievements.GetById(id);
            if(result == null){
                return NotFound("Achievement not found");
            }
            _mapper.Map(achievement, result);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

    }
}