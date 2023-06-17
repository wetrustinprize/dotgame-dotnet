using DotGameMemoryServer;
using Microsoft.AspNetCore.Mvc;

namespace DotGameRestful.Controllers;

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
    /// Creates a new game instance.
    /// </summary>
    /// <response code="200">Returns the ID of the new game instance.</response>
    [HttpPost]
    public Task<Guid> NewGame() => Task.FromResult(_memoryServer.CreateInstance().Id);
}