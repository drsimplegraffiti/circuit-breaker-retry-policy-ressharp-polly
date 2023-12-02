

namespace FormulaOne.Entities.DbSet
{
    public class Achievement: BaseEntity
    {
        public int RaceWins { get; set; }
        public int PolePosition { get; set; }
        public int FastestLap { get; set; }
        public int WorldChampionship { get; set; }

        public Guid DriverId { get; set; } // Foreign Key

        public virtual Driver? Driver { get; set; } // Navigation Property
    }
}