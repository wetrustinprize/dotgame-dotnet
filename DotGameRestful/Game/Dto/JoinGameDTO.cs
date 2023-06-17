using System.ComponentModel.DataAnnotations;

namespace DotGameRestful.Game.Dto;

/// <summary>
/// Information to join a game.
/// </summary>
public struct JoinGameDto
{
    /// <summary>
    /// Your display name.
    /// </summary>
    [Required]
    [MinLength(4)]
    public string Username { get; init; }
}