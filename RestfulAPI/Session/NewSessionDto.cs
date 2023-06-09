using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Session;

/// <summary>
/// Information needed to create a new session GUID
/// </summary>
public class NewSessionDto
{
    /// <summary>
    /// The display name of the player.
    /// </summary>
    [Required]
    public string Username { get; set; } = null!;
}