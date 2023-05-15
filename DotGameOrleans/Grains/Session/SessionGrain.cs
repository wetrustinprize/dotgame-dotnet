using DotGameOrleans.Grains.Interfaces;
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
        if (!State.Initialized)
            throw new SessionNotInitilized(this.GetPrimaryKey().ToString());
        
        return Task.FromResult(State);
    }
}