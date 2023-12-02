using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOne.Entities.DbSet
{
    public abstract class BaseEntity // abstract class cannot be instantiated
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public int Status { get; set; } 
    }
}