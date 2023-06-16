namespace DotGameLogic;

public struct Player
{
    /// <summary>
    /// The player's unique identifier
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The player data in a string format
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// Has this player left the game? Should it be skipped?
    /// </summary>
    public bool LeftGame { get; set; }

    public Player()
    {
        Id = Guid.NewGuid();
        Data = null!;
        LeftGame = false;
    }

    /// <summary>
    /// Generate the list of players using a list of string data
    /// </summary>
    public static List<Player> FromData(IEnumerable<object?> data)
    {
        return data.Select(d => new Player { Data = d }).ToList();
    }
}