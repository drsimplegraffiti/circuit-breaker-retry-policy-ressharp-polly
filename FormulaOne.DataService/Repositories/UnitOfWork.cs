using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly AppDbContext _context;
        public IDriverRepository Drivers { get; }

        public IAchievementRepository Achievements { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Drivers = new DriverRepository(_context, logger);
            Achievements = new AchievementRepository(_context, logger);

        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync(); 
            return result > 0;
        }


        // this is to dispose the context using Garbage Collector
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}