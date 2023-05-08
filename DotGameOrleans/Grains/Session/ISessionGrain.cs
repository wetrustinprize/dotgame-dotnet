namespace DotGameOrleans.Grains.Interfaces;

[GenerateSerializer]
public class SessionGrainState
{
    [Id(0)] public string Username { get; set; } = "";

    [Id(1)] public bool Initialized { get; set; }
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
    public Task<SessionGrainState> GetState();
}