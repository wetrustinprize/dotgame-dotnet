using System.ComponentModel.DataAnnotations;
using DotGameOrleans.Grains.Lobby;

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
    public HashSet<Guid> Players { get; init; } = null!;

    /// <summary>
    /// The current state of this lobby
    /// </summary>
    [Required]
    public LobbyStateEnum State { get; init; }
}