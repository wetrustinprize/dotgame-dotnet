namespace DotGameOrleans.Grains.Session;

[GenerateSerializer]
public class SessionGrainState
{
    [Id(0)] public string Username { get; init; } = "";

    [Id(1)] public bool Initialized { get; init; }
    
    [Id(2)] public List<Guid> Lobbies { get; } = new();
}

public interface ISessionGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Initialize the session grain with the given state.
    /// </summary>
    /// <param name="username">The session's username</param>
    public Task Init(string username);
    
    /// <summary>
    /// Gets the current state of a session
    /// </summary>
    /// <exception cref="SessionNotInitialized">If this grain was not been initialized yet</exception>
    public Task<SessionGrainState> GetState();
    
    /// <summary>
    /// Adds a new lobby to the session
    /// </summary>
    /// <param name="lobbyId">The lobby to be added</param>
    public Task AddLobby(Guid lobbyId);
}