
using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        // readonly because we are not going to change the value of this property
        public readonly ILogger _logger; 
        protected AppDbContext _context; // protected because we want to use it in the derived class
        internal DbSet<T> _dbSet; // internal because we want to use it in the same assembly

        public GenericRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>(); // or use _dbSet = _context.Set<T>();
        }
     
        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T?> GetById(Guid id)
        {
           return await _dbSet.FindAsync(id);
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}