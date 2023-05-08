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

    [GraphQLDescription("Returns a new session ID with the given username.")]
    public Task<Guid> CreateSession(string username)
    {
        var sessionId = Guid.NewGuid();
        var sessionGrain = _grainFactory.GetGrain<ISessionGrain>(sessionId);

        sessionGrain.Init(username);
        return Task.FromResult(sessionId);
    }
}