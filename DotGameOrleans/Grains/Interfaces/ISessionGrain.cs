namespace DotGameOrleans.Grains.Interfaces;

[GenerateSerializer]
public class SessionGrainState
{
    [Id(0)]
    public string Username { get; set; }

    public SessionGrainState(string username)
    {
        Username = username;
    }
}

public interface ISessionGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Initialize the session grain with the given state.
    /// </summary>
    /// <param name="state">The state of the new session</param>
    public Task Init(SessionGrainState state);
    
    /// <summary>
    /// Gets the current state of a session
    /// </summary>
    public Task<SessionGrainState> GetState();
}