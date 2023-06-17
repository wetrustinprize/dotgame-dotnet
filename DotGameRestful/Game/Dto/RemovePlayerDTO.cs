using System.ComponentModel.DataAnnotations;

namespace DotGameRestful.Game.Dto;

/// <summary>
/// Information for removing a player from a game.
/// </summary>
public struct RemovePlayerDTO
{
    /// <summary>
    /// The player guid to remove.
    /// </summary>
    [Required]
    public Guid PlayerId { get; init; }
}