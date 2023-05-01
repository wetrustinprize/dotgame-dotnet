using DotGameOrleans.Grains.Interfaces;
using HotChocolate.Language;

namespace DotGameGraphQL.Session;

[ExtendObjectType(OperationType.Mutation)]
public class SessionMutation
{
    private readonly ILogger<SessionMutation> _logger;
    private readonly IGrainFactory _grainFactory;

    public SessionMutation(ILogger<SessionMutation> logger, IGrainFactory grainFactory)
    {
        _logger = logger;
        _grainFactory = grainFactory;
    }

    public Task<Guid> CreateSession(string username)
    {
        var sessionId = Guid.NewGuid();
        var sessionGrain = _grainFactory.GetGrain<ISessionGrain>(sessionId);

        sessionGrain.Init(new()
        {
            Username = username
        });
        return Task.FromResult(sessionId);
    }
}