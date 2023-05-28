namespace ColourMemoryGame2.Models
{
    public class ColourMemoryGame
    {
        private int NumberOfGrids;

        public Guid Id { get; set; }
        public List<Grid> Grids { get; set; }
        public GameState CurrentStateOfGame { get; set; }

        private readonly List<string> colors = new List<string>
        {
            "Red",
            "Green",
            "Blue",
            "Yellow",
            "Purple",
            "Brown",
            "Black",
            "Orange"
        };

        public void StartNewGame()
        {
            NumberOfGrids = colors.Count * 2;
            InitiateGrids();
            CurrentStateOfGame = new GameState();
            CurrentStateOfGame.SetActiveCards(Grids.Count(g => g.Active));
            Id = Guid.NewGuid();
        }
        
        public Grid FindGrid(int gridId) =>
            Grids?.FirstOrDefault(g => g.Id == gridId);


        public bool CheckMatchBetweenTwoGrids(Grid grid1, Grid grid2)
        {
            var result = grid1.Color == grid2.Color;

            grid1.Active = false;
            grid2.Active = false;

            return result;
        }

        public bool CheckIfGameIsComplete() =>
            Grids.All(g => !g.Active);

        private void InitiateGrids()
        {
            var grids = new List<Grid>(NumberOfGrids);

            var uniqueIds = GenerateUniqueIds(0, 100);
            var uniqueIdIndex = 0;

            foreach (var color in colors)
            {
                var grid1 = CreateGrid(color);
                grid1.Id = uniqueIds[uniqueIdIndex++];
                grids.Add(grid1);

                var grid2 = CreateGrid(color);
                grid2.Id = uniqueIds[uniqueIdIndex++];
                grids.Add(grid2);
            }

            Grids = grids;
        }

        private Grid CreateGrid(string color)
        {
            return new Grid
            {
                Active = true,
                Color = color
            };
        }

        private List<int> GenerateUniqueIds(int min, int max)
        {
            if (NumberOfGrids > max - min + 1)
            {
                throw new ArgumentException("The number of unique numbers must be less than or equal to the range between min and max.");
            }

            var random = new Random();
            var uniqueNumbers = new HashSet<int>();

            while (uniqueNumbers.Count < NumberOfGrids)
            {
                int randomNumber = random.Next(min, max + 1);
                uniqueNumbers.Add(randomNumber);
            }

            return new List<int>(uniqueNumbers);
        }
    }
}
