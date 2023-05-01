using DotGameGraphQL.Types;
using DotGameOrleans.Grains.Interfaces;
using HotChocolate.Execution;
using HotChocolate.Language;

namespace DotGameGraphQL.Session;

[ExtendObjectType(OperationType.Query)]
public class SessionQuery
{
    private readonly ILogger<SessionQuery> _logger;
    private readonly IGrainFactory _grainFactory;

    public SessionQuery(ILogger<SessionQuery> logger, IGrainFactory grainFactory)
    {
        _logger = logger;
        _grainFactory = grainFactory;
    }

    public async Task<SessionStateType> GetMe(Guid session)
    {
        var sessionGrain = _grainFactory.GetGrain<ISessionGrain>(session);
        var state = await sessionGrain.GetState();

        if (!state.IsInitialized)
            throw new GraphQLException(SessionErrors.SessionNotFound);
        
        return new SessionStateType
        {
            Username = state.Username
        };
    }
}