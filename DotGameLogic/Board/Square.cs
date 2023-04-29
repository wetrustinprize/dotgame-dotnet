using DotGameLogic.Board.Exceptions;

namespace DotGameLogic.Board;

public class Square
{
    #region Private
    
    private readonly Dictionary<Position, Square> _neighbors = new();
    private readonly Dictionary<Position, int> _lines = new()
    {
        { Position.Top, -1 },
        { Position.Bottom, -1 },
        { Position.Left, -1 },
        { Position.Right, -1 }
    };

    #endregion
    
    /// <summary>
    /// The owner player index of this square.
    /// </summary>
    public int Owner { get; private set; } = -1;
    
    /// <summary>
    /// The square has all lines completed or has an owner.
    /// </summary>
    public bool IsCompleted
    {
        get
        {
            if (Owner != -1) return true;

            var top = GetLine(Position.Top) != -1;
            var bottom = GetLine(Position.Bottom) != -1;
            var left = GetLine(Position.Left) != -1;
            var right = GetLine(Position.Right) != -1;
            
            return top && bottom && left && right;
        }
    }

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
        
        _neighbors[position] = square;
        _lines.Remove(position);
    }
    
    /// <summary>
    /// Check if has a connection in the specific position
    /// </summary>
    /// <param name="position">The position to check the connection</param>
    /// <returns>True if has a connection</returns>
    public bool HasConnection(Position position) => _neighbors.ContainsKey(position);

    /// <summary>
    /// Sets the owner of the line in the specific position.
    /// </summary>
    /// <param name="position">The position to set the owner to</param>
    /// <param name="player">The owner player index</param>
    public void SetLine(Position position, int player)
    {
        // Check if has neighbor in that position
        if (_neighbors.TryGetValue(position, out var neighbor))
            neighbor.SetLine(position, player);
        else
            _lines[position] = player;

        if (IsCompleted)
            Owner = player;
    }
    
    /// <summary>
    /// Gets the owner of a specific line.
    /// </summary>
    /// <param name="position">The position to get the owner from</param>
    /// <returns>The owner player index</returns>
    public int GetLine(Position position)
    {
        return _neighbors.TryGetValue(position, out var neighbor)
            ? neighbor.GetLine(position.Invert())
            : _lines[position];
    }
}