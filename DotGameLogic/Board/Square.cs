using DotGameLogic.Board.Exceptions;

namespace DotGameLogic.Board;

public class Square
{
    #region Lines

    private Dictionary<Position, Square> Neighbors { get; } = new();

    private Dictionary<Position, int> Lines { get; } = new()
    {
        { Position.Top, -1 },
        { Position.Bottom, -1 },
        { Position.Left, -1 },
        { Position.Right, -1 }
    };

    #endregion

    /// <summary>
    /// Connects two squares.
    /// Both squares will have a connection in the same position.
    /// </summary>
    /// <exception cref="AlreadyConnected">
    /// Thrown if <paramref name="square"/> already has a connection
    /// in the inverted position.
    /// </exception>
    /// <param name="position">The "line" to be connected</param>
    /// <param name="square">The square to connect to</param>
    public void ConnectSquare(Position position, Square square)
    {
        // Check if the neighbor already has a connection
        if (square.HasConnection(position.Invert()))
            throw new AlreadyConnected();
        
        Neighbors[position] = square;
        Lines.Remove(position);
    }
    
    /// <summary>
    /// Check if has a connection in the specific position
    /// </summary>
    /// <param name="position">The position to check the connection</param>
    /// <returns>True if has a connection</returns>
    public bool HasConnection(Position position) => Neighbors.ContainsKey(position);

    /// <summary>
    /// Sets the owner of the line in the specific position.
    /// </summary>
    /// <param name="position">The position to set the owner to</param>
    /// <param name="player">The owner player index</param>
    public void SetLine(Position position, int player)
    {
        // Check if has neighbor in that position
        if (Neighbors.TryGetValue(position, out var neighbor))
            neighbor.SetLine(position, player);
        else
            Lines[position] = player;
    }
    
    /// <summary>
    /// Gets the owner of a specific line.
    /// </summary>
    /// <param name="position">The position to get the owner from</param>
    /// <returns>The owner player index</returns>
    public int GetLine(Position position)
    {
        return Neighbors.TryGetValue(position, out var neighbor)
            ? neighbor.GetLine(position.Invert())
            : Lines[position];
    }
}