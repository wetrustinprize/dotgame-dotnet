namespace DotGameOrleans.Grains.Session;

[GenerateSerializer]
public class SessionGrainState
{
    [Id(0)] public string Username { get; init; } = "";

    [Id(1)] public bool Initialized { get; init; }
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
}