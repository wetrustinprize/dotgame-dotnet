using DotGameOrleans.Grains.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestfulAPI.Session;

[ApiController]
[Route("session")]
public class SessionController
{
    private readonly ILogger<SessionController> _logger;
    private readonly IGrainFactory _grainFactory;

    public SessionController(ILogger<SessionController> logger, IGrainFactory grainFactory)
    {
        _logger = logger;
        _grainFactory = grainFactory;
    }
    
    /// <summary>
    /// Generates a new session with a unique guid
    /// </summary>
    /// <remarks>
    /// This session guid is used to identify the player in the game
    /// </remarks>
    /// <param name="data">The information about the player</param>
    /// <returns>The session guid</returns>
    /// <response code="200">Returns the session guid</response>
    [HttpPost(Name = "createSession")]
    [ProducesResponseType(typeof(Guid), 200)]
    public Guid CreateSession([FromBody] NewSessionDto data)
    {
        var sessionId = Guid.NewGuid();
        var grain = _grainFactory.GetGrain<ISessionGrain>(sessionId);

        grain.Init(data.Username);
        return sessionId;
    }
}