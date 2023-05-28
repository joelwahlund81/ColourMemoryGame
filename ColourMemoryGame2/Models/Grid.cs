using Newtonsoft.Json;

namespace ColourMemoryGame2.Models
{
    public class Grid
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}
