using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IDriverRepository Drivers { get; } // we are using get because we are not going to set the value of this property
        IAchievementRepository Achievements { get; }
        Task<bool> CompleteAsync();
    }
}