using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Controllers.v1.Lobby;

/// <summary>
/// A lobby information response
/// </summary>
public class LobbyResponse
{
    /// <summary>
    /// The current players of this lobby
    /// </summary>
    [Required]
    public HashSet<Guid> Players { get; set; } = null!;
}