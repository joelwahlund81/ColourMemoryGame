using ColourMemoryGame2.Models;
using ColourMemoryGame2.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ColourMemoryGame2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetNewGame(Guid? gameId)
        {
            ColourMemoryGame game;

            if (gameId.HasValue)
            {
                game = _gameService.GetGame(gameId.Value);
            }
            else
            {
                game = _gameService.InitiateNewGame();
            }

            if (game == null && gameId.HasValue)
            {
                return NotFound();
            }
            else if (game == null)
            {
                return BadRequest();
            }

            return Ok(game);
        }

        [HttpGet("grid")]
        public IActionResult GetGrid(Guid? gameId, int? gridId)
        {
            if (!gameId.HasValue) return BadRequest("gameId was null");
            if (!gridId.HasValue) return BadRequest("gridId was null");

            var grid = _gameService.GetGrid(gameId.Value, gridId.Value);

            if (grid == null)
            {
                return NotFound();
            }

            return Ok(grid);
        }

        [HttpPut]
        public IActionResult CheckTwoGrids([FromBody] CheckGrids checkGridsModel)
        {
            if (!checkGridsModel.AllValuesSet())
            {
                return BadRequest("Something went wrong, please check all values again");
            }

            var result = _gameService
                .CheckMatchBetweenGrids(
                    checkGridsModel.GameId.Value,
                    checkGridsModel.Grid1.Value,
                    checkGridsModel.Grid2.Value);

            if (result == null)
            {
                return BadRequest("Something went wrong when checking grids");
            }

            return Ok(result);
        }
    }
}