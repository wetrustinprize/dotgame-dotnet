namespace DotGameOrleans.Grains.Game;

[GenerateSerializer]
public class GamePlayer
{
    /// <summary>
    /// The session of the player
    /// </summary>
    [Id(0)]
    public Guid Session { get; init; }

    /// <summary>
    /// Has the player left the game
    /// </summary>
    [Id(1)]
    public bool Left { get; set; } = false;
}