using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FormulaOne.Entities.DbSet
{
    public class Driver: BaseEntity
    {

        public Driver()
        {
            Achievements = new HashSet<Achievement>(); // This will help us to avoid NullReferenceException
        }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int DriverNumber { get; set; }
        public DateTime DateOfBirth { get; set; } 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // This will help us to avoid NullReferenceException
        public virtual ICollection<Achievement> Achievements { get; set; } // we are using virtual keyword to enable lazy loading, lazy loading is a technique that defers the loading of connected objects, until we need them
    }
}