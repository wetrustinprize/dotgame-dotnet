using DotGameOrleans.Grains.Session;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable UnusedMember.Global

namespace RestfulAPI.Session;

[Route("api/[controller]")]
[ApiController]
public class SessionController : Controller
{
    private readonly ILogger<SessionController> _logger;
    private readonly IGrainFactory _grainFactory;

    public SessionController(ILogger<SessionController> logger, IGrainFactory grainFactory)
    {
        _logger = logger;
        _grainFactory = grainFactory;
    }

    /// <summary>
    /// Generates a new session with a unique GUID
    /// </summary>
    /// <remarks>
    /// This session guid is used to identify the player in the game
    /// </remarks>
    /// <param name="data">The information about the player</param>
    /// <returns>The session guid</returns>
    /// <response code="200">Returns the session guid</response>
    [HttpPost]
    public async Task<Guid> CreateSession([FromBody] NewSessionDto data)
    {
        var sessionId = Guid.NewGuid();
        var grain = _grainFactory.GetGrain<ISessionGrain>(sessionId);

        await grain.Init(data.Username);
        return sessionId;
    }

    /// <summary>
    /// Gets the information about a session
    /// </summary>
    /// <param name="sessionId">The session GUID to get the information from</param>
    /// <returns>The information about the session in response format</returns>
    /// <response code="200">Returns the information about the session</response>
    [HttpGet("{sessionId:guid}")]
    [ProducesResponseType(typeof(GetSessionResponse), statusCode: 200)]
    public async Task<GetSessionResponse> GetSession(Guid sessionId)
    {
        var grain = _grainFactory.GetGrain<ISessionGrain>(sessionId);
        var state = await grain.GetState();

        return new GetSessionResponse
        {
            Username = state.Username
        };
    }
}