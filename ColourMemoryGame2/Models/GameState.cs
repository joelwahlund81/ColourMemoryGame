namespace ColourMemoryGame2.Models
{
    public class GameState
    {
        private readonly int StartingPoints = 0;

        private bool Locked { get; set; }
        private bool GridsLocked { get; set; }
        public int Points { get; set; } = 0;
        public int ActiveCards { get; private set; }

        public void LockGrids()
        {
            GridsLocked = true;
        }

        public void UnlockGrids()
        {
            GridsLocked = false;
        }

        public void AddOnePoint()
        {
            Points++;
        }

        public void RemoveOnePoint()
        {
            Points--;
        }

        public void InitiateFreshState()
        {
            Locked = false;
            GridsLocked = false;
            Points = StartingPoints;
        }

        public void CompleteGame()
        {
            Locked = true;
            GridsLocked = true;
        }

        public void SetActiveCards(int activeCards)
        {
            ActiveCards = activeCards;
        }
    }
}
