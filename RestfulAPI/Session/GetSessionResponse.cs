using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Session;

// ReSharper disable UnusedAutoPropertyAccessor.Global

/// <summary>
/// Response for getting information about a session GUID
/// </summary>
public class GetSessionResponse
{
    /// <summary>
    /// The display name of the player.
    /// </summary>
    [Required]
    public string Username { get; set; } = null!;
}