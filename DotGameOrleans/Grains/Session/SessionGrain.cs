using Microsoft.Extensions.Logging;

namespace DotGameOrleans.Grains.Session;

public class SessionGrain : Grain<SessionGrainState>, ISessionGrain
{
    private readonly ILogger _logger;

    public SessionGrain(ILogger<SessionGrain> logger)
    {
        _logger = logger;

        _logger.LogDebug($"New state with grain id {this.GetPrimaryKey()}");
    }

    #region Checkers

    /// <summary>
    /// Checks if this grain was initialized
    /// </summary>
    /// <exception cref="SessionNotInitialized">Raises if there was no initialization</exception>
    private void CheckInitialized()
    {
        if (!State.Initialized)
            throw new SessionNotInitialized(this.GetPrimaryKey().ToString());
    }

    #endregion

    public Task Init(string username)
    {
        State = new SessionGrainState
        {
            Username = username,
            Initialized = true
        };

        _logger.LogDebug($"Setting new state with grain id {this.GetPrimaryKey()}");
        return Task.CompletedTask;
    }

    public Task<SessionGrainState> GetState()
    {
        CheckInitialized();
        return Task.FromResult(State);
    }

    public Task AddLobby(Guid lobbyId)
    {
        State.Lobbies.Add(lobbyId);

        return Task.CompletedTask;
    }
}