using DotGameLogic;

namespace DotGameOrleans.Grains.Lobby;

public class LobbyGrain : Grain<LobbyGrainState>, ILobbyGrain
{
    private readonly ILogger _logger;

    public LobbyGrain(ILogger<LobbyGrain> logger)
    {
        _logger = logger;
    }

    #region Checkers

    private void CheckInitialized()
    {
        if (!State.Initialized)
            throw new LobbyNotInitialized(this.GetPrimaryKey());
    }

    #endregion

    public Task Init(Guid owner, int boardHeight, int boardWidth)
    {
        _logger.LogDebug("Creating new lobby for user {Owner}", owner);

        State = new LobbyGrainState
        {
            Initialized = true,
            Owner = owner,
            Board = new Board(boardHeight, boardWidth)
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

    public void StartGame()
    {
        CheckInitialized();

        if (State.Players.Count <= 1)
            throw new NotEnoughPlayers(this.GetPrimaryKey());

        if (State.State != LobbyStateEnum.WaitingForPlayers)
            throw new LobbyInProgress(this.GetPrimaryKey());

        State.State = LobbyStateEnum.InProgress;
    }

    public void AddPlayer(Guid session)
    {
        CheckInitialized();
        if (State.State != LobbyStateEnum.WaitingForPlayers)
            throw new LobbyInProgress(this.GetPrimaryKey());

        if (State.Players.Any(p => p == session))
            throw new LobbyAlreadyJoined(this.GetPrimaryKey(), session);

        State.Players.Add(session);
    }

    public void RemovePlayer(Guid session)
    {
        CheckInitialized();
        if (State.Players.All(p => p != session))
            throw new NotInLobby(this.GetPrimaryKey(), session);

        switch (State.State)
        {
            case LobbyStateEnum.WaitingForPlayers:
                State.Players.Remove(session);
                break;
            case LobbyStateEnum.InProgress:
                // TODO: Mark the player as "out of game", skip it's turn
                break;
            case LobbyStateEnum.Finished:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}