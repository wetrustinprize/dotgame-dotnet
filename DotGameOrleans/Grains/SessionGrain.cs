using DotGameOrleans.Grains.Interfaces;

namespace DotGameOrleans.Grains;

public class SessionGrain : Grain<SessionGrainState>, ISessionGrain
{
    private readonly ILogger _logger;
    
    public SessionGrain(ILogger<SessionGrain> logger)
    {
        _logger = logger;
        
        _logger.LogDebug($"New state with grain id {this.GetPrimaryKey()}");
    }

    public Task Init(SessionGrainState state)
    {
        state.IsInitialized = true;
        State = state;
        
        _logger.LogDebug($"Setting new state with grain id {this.GetPrimaryKey()}");
        return Task.CompletedTask;
    }

    public Task<SessionGrainState> GetState() => Task.FromResult(State);
}