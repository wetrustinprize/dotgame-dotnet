using DotGameLogic.DotGame.Exceptions;

namespace DotGameLogic.DotGame;

/// <summary>
/// The main class for a DotGame.
/// </summary>
public class DotGame
{
    public int Height { get; }
    public int Width { get; }
    
    public int TotalPlayers { get; }
    public int CurrentPlayer { get; set; }

    /// <summary>
    /// Create a new DotGame.
    /// </summary>
    /// <param name="height">Height of the board</param>
    /// <param name="width">Width of the board</param>
    /// <param name="initialPlayers">The amount of players in this game</param>
    public DotGame(int height, int width, int initialPlayers = 2)
    {
        if (initialPlayers <= 0)
            throw new NotEnoughPlayers();
        
        Height = height;
        Width = width;
        TotalPlayers = initialPlayers;
        CurrentPlayer = 0;
    }
}