using ColourMemoryGame2.Models;
using ColourMemoryGame2.Services.Interfaces;

namespace ColourMemoryGame2.Services
{
    public class GameService : IGameService
    {
        private readonly IDataService _dataService;
        private readonly ILogger<GameService> _logger;

        public GameService(IDataService dataService, ILogger<GameService> logger)
        {
            this._dataService = dataService;
            _logger = logger;
        }

        public GameState CheckMatchBetweenGrids(Guid gameId, int gridId1, int gridId2)
        {
            _logger.LogInformation("Trying to check for match between Grid '{gridId1}' and Grid '{gridId2}' on Game '{gameId}'", gridId1, gridId2, gameId);
            var game = GetGame(gameId);

            if (game == null)
            {
                _logger.LogError("Game '{gameId}' was not found", gameId);
                
                return null;
            }

            game.CurrentStateOfGame.LockGrids();
            SaveGame(game);

            var grid1 = game.FindGrid(gridId1);
            if (grid1 == null)
            {
                _logger.LogError("Grid1 '{gridId1}' was not found on game '{gameId}'", gridId1, gameId);
                return null;
            } 
            else
            {
                if (!grid1.Active)
                {
                    _logger.LogError("Grid1 '{gridId1}' is not active on game '{gameId}'", gridId1, gameId);
                    return null;
                }
            }

            var grid2 = game.FindGrid(gridId2);
            if (grid2 == null)
            {
                _logger.LogError("Grid2 '{gridId2}' was not found on game '{gameId}'", gridId2, gameId);
                return null;
            }
            else
            {
                if (!grid2.Active)
                {
                    _logger.LogError("Grid2 '{gridId2}' is not active on game '{gameId}'", gridId2, gameId);
                    return null;
                }
            }

            var isSameColor = game.CheckMatchBetweenTwoGrids(grid1, grid2);

            if (isSameColor)
            {
                _logger.LogInformation("Grid '{gridId1}' and Grid '{gridId2}' was of same color", gridId1, gridId2);

                game.CurrentStateOfGame.AddOnePoint();
            }
            else
            {
                _logger.LogInformation("Grid '{gridId1}' and Grid '{gridId2}' was not of same color", gridId1, gridId2);

                game.CurrentStateOfGame.RemoveOnePoint();
            }

            game.CurrentStateOfGame.UnlockGrids();
            SaveGame(game);


            _logger.LogInformation("Checking if Game '{gameId}' is completed", gameId);

            if (game.CheckIfGameIsComplete())
            {
                SaveGame(game);

                _logger.LogInformation("Game '{gameId}' is completed!", gameId);
            };

            game.CurrentStateOfGame.SetActiveCards(game.Grids.Count(x => x.Active));

            return game.CurrentStateOfGame;
        }

        public Grid GetGrid(Guid gameId, int gridId)
        {
            _logger.LogInformation("Trying to find Grid '{gridId}' on Game '{gameId}'", gridId, gameId);

            var game = GetGame(gameId);

            if (game == null)
            {
                _logger.LogError("Game '{gameId}' was not found", gameId);
                return null;
            }
            
            var grid = game.FindGrid(gridId);

            if (grid == null)
            {
                _logger.LogError("Grid '{gridId}' was not found on game '{gameId}'", gridId, gameId);
                return null;
            }

            _logger.LogInformation("Successfully found Grid '{gridId}' on Game '{gameId}'", gridId, gameId);

            return grid;
        }

        public ColourMemoryGame GetGame(Guid gameId)
        {
            var game = _dataService.LoadData(gameId);

            _logger.LogInformation("Trying to find Game '{gameId}'", gameId);

            return game;
        }

        public ColourMemoryGame InitiateNewGame()
        {
            _logger.LogInformation("Starting new game");
            var game = new ColourMemoryGame();
            game.StartNewGame();

            _logger.LogInformation("New game has id '{Id}'", game.Id);

            _dataService.SaveData(game);

            _logger.LogInformation("Saved new game");

            return game;
        }

        private void SaveGame(ColourMemoryGame game)
        {
            _dataService.SaveData(game);
        }
    }
}
