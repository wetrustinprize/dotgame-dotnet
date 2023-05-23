using DotGameLogic;
using DotGameOrleans.Grains.Session;
using Microsoft.Extensions.Logging;

namespace DotGameOrleans.Grains.Lobby;

public class LobbyGrain : Grain<LobbyGrainState>, ILobbyGrain
{
    private readonly ILogger _logger;
    private readonly IGrainFactory _grainFactory;

    public LobbyGrain(ILogger<LobbyGrain> logger, IGrainFactory grainFactory)
    {
        _logger = logger;
        _grainFactory = grainFactory;
    }

    public Task Init(Guid owner, int boardHeight, int boardWidth)
    {
        _logger.LogDebug($"Creating new lobby for user {owner}.");
        
        State.Owner = owner;
        State.Board = new Board(boardHeight, boardWidth);
        State.Players.Add(owner);

        var ownerSession = _grainFactory.GetGrain<ISessionGrain>(owner);
        ownerSession.AddLobby(this.GetPrimaryKey());
        
        return Task.CompletedTask;
    }

    public Task<Guid> GetOwner() => Task.FromResult(State.Owner);
}