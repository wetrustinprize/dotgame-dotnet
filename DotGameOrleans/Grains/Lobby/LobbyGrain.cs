using DotGameLogic;
using DotGameOrleans.Grains.Interfaces;

namespace DotGameOrleans.Grains;

public class LobbyGrain : Grain, ILobbyGrain
{
    private readonly ILogger _logger;
    private Board _board;
    
    public LobbyGrain(ILogger<LobbyGrain> logger)
    {
        _logger = logger;
    }
}