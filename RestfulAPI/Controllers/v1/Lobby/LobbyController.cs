using DotGameOrleans.Grains.Lobby;
using Microsoft.AspNetCore.Mvc;

namespace RestfulAPI.Lobby;

[Route("api/[controller]")]
[ApiController]
public class LobbyController : Controller
{
    private readonly ILogger<LobbyController> _logger;
    private readonly IGrainFactory _grainFactory;

    public LobbyController(ILogger<LobbyController> logger, IGrainFactory grainFactory)
    {
        _logger = logger;
        _grainFactory = grainFactory;
    }

    /// <summary>
    /// Creates a new lobby and joins it
    /// </summary>
    /// <param name="session">The session GUID</param>
    /// <param name="data">The data for creating the new lobby</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Guid> CreateLobby(Guid session, [FromBody] CreateLobbyDto data)
    {
        var lobbyGuid = Guid.NewGuid();
        var lobbyGrain = _grainFactory.GetGrain<ILobbyGrain>(lobbyGuid);

        await lobbyGrain.Init(session, data.Height, data.Width);
        
        return lobbyGuid;
    }

    [HttpPost]
    [Route("{lobby:guid}/join")]
    public async Task<LobbyResponse> JoinLobby(Guid session, Guid lobby)
    {
        var lobbyGrain = _grainFactory.GetGrain<ILobbyGrain>(lobby);
        var lobbyState = await lobbyGrain.GetState();

        return new LobbyResponse
        {
            Players = lobbyState.Players
        };
    }
}