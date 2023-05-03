using DotGameOrleans.Grains.Interfaces;

namespace DotGameOrleans.Grains;

public class SessionGrain : Grain, ISessionGrain
{
    private readonly ILogger _logger;
    private SessionGrainState? _state;
    
    public SessionGrain(ILogger<SessionGrain> logger)
    {
        _logger = logger;
        
        _logger.LogDebug($"New state with grain id {this.GetPrimaryKey()}");
    }

    public Task Init(SessionGrainState state)
    {
        _state = state;
        
        _logger.LogDebug($"Setting new state with grain id {this.GetPrimaryKey()}");
        return Task.CompletedTask;
    }

    public Task<SessionGrainState> GetState()
    {
        if (_state == null)
            throw new Exception($"State from grain {this.GetPrimaryKey()} is null");
        
        return Task.FromResult(_state);
    }
}