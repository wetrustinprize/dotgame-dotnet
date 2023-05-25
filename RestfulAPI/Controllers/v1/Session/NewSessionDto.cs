using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Controllers.v1.Session;

/// <summary>
/// Information needed to create a new session GUID
/// </summary>
public class NewSessionDto
{
    /// <summary>
    /// The display name of the session.
    /// </summary>
    [Required]
    public string Username { get; set; } = null!;
}