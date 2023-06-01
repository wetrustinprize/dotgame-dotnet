namespace DotGameLogic;

public struct Player
{
    /// <summary>
    /// The player data in a string format
    /// </summary>
    public string Data { get; init; }

    /// <summary>
    /// Has this player left the game? Should it be skipped?
    /// </summary>
    public bool LeftGame { get; set; }

    /// <summary>
    /// Generate the list of players using a list of string data
    /// </summary>
    public static List<Player> FromData(IEnumerable<string> data)
    {
        return data.Select(d => new Player { Data = d }).ToList();
    }
}