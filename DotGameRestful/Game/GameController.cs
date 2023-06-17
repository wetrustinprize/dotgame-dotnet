using DotGameMemoryServer;
using DotGameRestful.Game.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS1591

namespace DotGameRestful.Game;

[ApiController]
[Route("game")]
public class GameController : Controller
{
    private readonly MemoryServer _memoryServer;

    public GameController(MemoryServer memoryServer)
    {
        _memoryServer = memoryServer;
    }

    /// <summary>
    /// Creates a new game.
    /// </summary>
    /// <response code="200">Returns the ID of the new game.</response>
    [HttpPost]
    [Route("")]
    public ActionResult<Guid> NewGame() => _memoryServer.CreateInstance().Id;

    /// <summary>
    /// Joins a game.
    /// </summary>
    /// <remarks>
    /// Use the returned ID as a authentication token.
    /// </remarks>
    /// <response code="200">Returns the ID of the newly created player.</response>
    /// <response code="404">The informed Game guid was not found.</response>
    [HttpPost]
    [Route("{gameId:guid}/join")]
    public ActionResult<Guid> JoinGame([FromBody] JoinGameDto dto, Guid gameId)
    {
        var instance = _memoryServer.GetInstance(gameId);

        if (instance == null)
            return NotFound();

        var player = instance.AddPlayer(dto);

        return player.Id;
    }

    /// <summary>
    /// Gets information about a specific Game
    /// </summary>
    /// <response code="200">Successfully got the game information.</response>
    /// <response code="404">The specified game was not found.</response>
    /// <response code="401">You left or has been kicked out of this game.</response>
    [HttpGet]
    [Route("{gameId:guid}"), Authorize]
    public ActionResult<object> GetGame(Guid gameId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Leaves the specified Game.
    /// </summary>
    /// <response code="200">Successfully left the game.</response>
    /// <response code="404">The specified game was not found.</response>
    /// <response code="401">You've already left or has been kicked out of this game.</response>
    [HttpPost]
    [Route("{gameId:guid}/leave"), Authorize]
    public ActionResult LeaveGame(Guid gameId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes a players from the specified game.
    /// </summary>
    /// <response code="200">Successfully removed the player from the game.</response>
    /// <response code="404">The specified game was not found, or you're not in this game.</response>
    /// <response code="401">You're not the owner of this game.</response>
    [HttpPost]
    [Route("{gameId:guid}/remove"), Authorize]
    public ActionResult RemovePlayer([FromBody] RemovePlayerDTO dto, Guid gameId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Plays a line in the specified game.
    /// </summary>
    /// <response code="200">Successfully played in the game board.</response>
    /// <response code="404">The specified game was not found, or you're not in this game.</response>
    /// <response code="401">It's not your turn, or you've left/kicked from the game.</response>
    [HttpPost]
    [Route("{gameId:guid}/play"), Authorize]
    public ActionResult Play(Guid gameId)
    {
        throw new NotImplementedException();
    }
}