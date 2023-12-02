using FormulaOne.Entities.DbSet;

namespace FormulaOne.DataService.Repositories.Interfaces
{
    public interface IAchievementRepository: IGenericRepository<Achievement>
    {
        Task<Achievement?> GetByDriverAchievementAsync(Guid driverId);
    }
}