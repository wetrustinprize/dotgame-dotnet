using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Controllers.v1.Session;

/// <summary>
/// Response for getting information about a session GUID
/// </summary>
public struct GetSessionResponse
{
    /// <summary>
    /// The display name of the session.
    /// </summary>
    [Required]
    public string Username { get; set; }
}