using DotGameLogic.Exceptions;

namespace DotGameLogic;

/// <summary>
/// The main class for a DotGame.
/// </summary>
public class DotGame
{
    public const int MinimumPlayers = 2;

    public int TotalPlayers { get; }
    public int CurrentPlayer { get; }
    public Board Board { get; }
    
    /// <summary>
    /// Create a new DotGame.
    /// </summary>
    /// <param name="height">Height of the board</param>
    /// <param name="width">Width of the board</param>
    /// <param name="initialPlayers">The amount of players in this game</param>
    public DotGame(int height, int width, int initialPlayers = 2)
    {
        if (initialPlayers <= 1)
            throw new NotEnoughPlayers(initialPlayers, MinimumPlayers);

        Board = new Board(height, width);
        TotalPlayers = initialPlayers;
        CurrentPlayer = 0;
    }
}