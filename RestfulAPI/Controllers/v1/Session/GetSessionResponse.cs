using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Controllers.v1.Session;

// ReSharper disable UnusedAutoPropertyAccessor.Global

/// <summary>
/// Response for getting information about a session GUID
/// </summary>
public class GetSessionResponse
{
    /// <summary>
    /// The display name of the session.
    /// </summary>
    [Required]
    public string Username { get; set; } = null!;
    
    /// <summary>
    /// The lobbies this session is in.
    /// </summary>
    [Required]
    public List<Guid> Lobbies { get; set; } = null!;
}