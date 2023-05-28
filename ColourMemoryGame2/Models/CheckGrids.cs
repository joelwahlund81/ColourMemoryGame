namespace ColourMemoryGame2.Models
{
    public class CheckGrids
    {
        public Guid? GameId { get; set; }
        public int? Grid1 { get; set; }
        public int? Grid2 { get; set; }

        public bool AllValuesSet()
        {
            return 
                GameId.HasValue &&
                Grid1.HasValue &&
                Grid2.HasValue;
        }
    }
}
