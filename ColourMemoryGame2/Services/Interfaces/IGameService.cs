using ColourMemoryGame2.Models;

namespace ColourMemoryGame2.Services.Interfaces
{
    public interface IGameService
    {
        ColourMemoryGame InitiateNewGame();
        Grid GetGrid(Guid gameId, int gridId);
        ColourMemoryGame GetGame(Guid gameId);
        GameState CheckMatchBetweenGrids(Guid gameId, int gridId1, int gridId2);
    }
}
