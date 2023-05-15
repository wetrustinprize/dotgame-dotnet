using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Session;

public class NewSessionDto
{
    /// <summary>
    /// The display name of the player.
    /// </summary>
    [Required]
    [MinLength(3)]
    [MaxLength(18)]
    public string Username { get; set; } = null!;
}