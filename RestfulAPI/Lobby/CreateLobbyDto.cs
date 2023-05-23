using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Lobby;

/// <summary>
/// Information needed to create a new lobby
/// </summary>
public class CreateLobbyDto
{
    /// <summary>
    /// The board height
    /// </summary>
    [Required]
    [Range(2, 100)]
    public int Height { get; set; }
    
    /// <summary>
    /// The board width
    /// </summary>
    [Required]
    [Range(2, 100)]
    public int Width { get; set; }
}