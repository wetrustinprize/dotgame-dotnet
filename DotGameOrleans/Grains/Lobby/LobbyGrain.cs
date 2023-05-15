using DotGameLogic;
using DotGameOrleans.Grains.Interfaces;
using Microsoft.Extensions.Logging;

namespace DotGameOrleans.Grains.Lobby;

public class LobbyGrain : Grain, ILobbyGrain
{
    private readonly ILogger _logger;
    private Board _board;
    
    public LobbyGrain(ILogger<LobbyGrain> logger)
    {
        _logger = logger;
    }
}