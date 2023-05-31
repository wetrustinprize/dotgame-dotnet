using DotGameOrleans.Grains.Game;

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

    #region Checkers

    private void CheckInitialized()
    {
        if (!State.Initialized)
            throw new LobbyNotInitializedException(this.GetPrimaryKey());
    }

    #endregion

    public Task Init(Guid owner)
    {
        State = new LobbyGrainState
        {
            Initialized = true,
            Owner = owner
        };
        State.Players.Add(owner);

        return Task.CompletedTask;
    }

    public Task<Guid> GetOwner()
    {
        CheckInitialized();
        return Task.FromResult(State.Owner);
    }

    public Task<bool> IsOwner(Guid session)
    {
        CheckInitialized();
        return Task.FromResult(session == State.Owner);
    }

    public Task<LobbyGrainState> GetState()
    {
        CheckInitialized();
        return Task.FromResult(State);
    }

    public async Task StartGame(int height, int width)
    {
        CheckInitialized();

        if (State.Players.Count <= 1)
            throw new NotEnoughPlayersException(this.GetPrimaryKey());

        var gameGrain = _grainFactory.GetGrain<IGameGrain>(this.GetPrimaryKey());
        await gameGrain.Init(State.Players, height, width);

        await ClearStateAsync();
    }

    public Task AddPlayer(Guid session)
    {
        CheckInitialized();
        if (State.Players.Any(p => p == session))
            throw new LobbyAlreadyJoinedException(this.GetPrimaryKey(), session);

        State.Players.Add(session);

        return Task.CompletedTask;
    }

    public Task RemovePlayer(Guid session)
    {
        CheckInitialized();
        if (State.Players.All(p => p != session))
            throw new NotInLobbyException(this.GetPrimaryKey(), session);

        State.Players.Remove(session);

        return Task.CompletedTask;
    }
}