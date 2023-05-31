using DotGameLogic;

namespace DotGameOrleans.Grains.Game;

public class GameGrain : Grain<GameGrainState>, IGameGrain
{
    #region Checkers

    private void CheckInitialized()
    {
        if (!State.Initialized)
        {
            throw new GameNotInitializedException(this.GetPrimaryKey());
        }
    }

    /// <summary>
    /// Checks if a session is in the Players list and has not left the game.
    /// </summary>
    /// <param name="session">The session to check</param>
    /// <exception cref="NotInGameException">If the session does not exist or has left</exception>
    private void CheckInGame(Guid session)
    {
        var exists = State.Players.Any(player => player.Session == session);
        var left = State.Players.Any(player => player.Session == session && player.Left);

        if (!exists || left)
            throw new NotInGameException(this.GetPrimaryKey(), session);
    }

    #endregion

    public Task Init(IEnumerable<Guid> players, Guid owner, int height, int width)
    {
        var playersList = players.ToList();

        if (playersList.All(player => player != owner))
            throw new OwnerNotInGameException(this.GetPrimaryKey(), owner);

        State = new GameGrainState
        {
            Initialized = true,
            Owner = owner,
            Players = playersList.Select(session => new GamePlayer
            {
                Session = session
            }).ToList(),
            CurrentPlayerIndex = 0,
            Board = new Board(height, width)
        };

        return Task.CompletedTask;
    }

    public Task<GameGrainState> GetState()
    {
        CheckInitialized();

        return Task.FromResult(State);
    }

    public Task RemoveSession(Guid session)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOwner(Guid session) => Task.FromResult(session == State.Owner);
}