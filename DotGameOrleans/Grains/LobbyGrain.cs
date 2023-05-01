using DotGameOrleans.Grains.Interfaces;

namespace DotGameOrleans.Grains;

public class LobbyGrain : Grain, ILobbyGrain
{
    private readonly ILogger _logger;

    public LobbyGrain(ILogger<LobbyGrain> logger)
    {
        _logger = logger;
    }
}