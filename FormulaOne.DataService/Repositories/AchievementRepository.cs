using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories
{
    public class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(AppDbContext context, ILogger logger) : base(context, logger){}

        public async Task<Achievement?> GetByDriverAchievementAsync(Guid driverId)
        {
            try
            {
               var result = await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);
               if (result == null) return null!;
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,"{Repo} GetByDriverAchievementAsync function error",typeof(AchievementRepository));
                return null!;
            }
        }


         public override async Task<IEnumerable<Achievement>> All() // we are not using the actual All from the GenericRepository, we are overriding it
        {
            try
            {
                return await _dbSet.Where(x =>x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery() // this enable the split query , good for performance
                    .OrderBy(x => x.AddedAt)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,"{Repo} All function error",typeof(AchievementRepository));
                return null!;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null) return false;
                result.Status = 0;
                result.UpdatedDate = DateTime.UtcNow;
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,"{Repo} Delete function error",typeof(AchievementRepository));
                return false;
            }
        }

        public override async Task<bool> Update(Achievement achievement)
        {
             try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);
                if (result == null) return false;
                result.UpdatedDate = DateTime.UtcNow;
                result.DriverId = achievement.DriverId;
                result.PolePosition = achievement.PolePosition;
                result.FastestLap = achievement.FastestLap;
                result.RaceWins = achievement.RaceWins;
                result.WorldChampionship = achievement.WorldChampionship;
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,"{Repo} Delete function error",typeof(AchievementRepository));
                return false;
            }
        }
    }
}