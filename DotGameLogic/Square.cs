using DotGameLogic.Enums;
using DotGameLogic.Exceptions;

namespace DotGameLogic;

public class Square
{
    #region Private

    private readonly Dictionary<Position, Square> _neighbors = new();

    private readonly Dictionary<Position, Player?> _lines = new()
    {
        { Position.Top, null },
        { Position.Bottom, null },
        { Position.Left, null },
        { Position.Right, null }
    };

    #endregion

    /// <summary>
    /// The owner player index of this square.
    /// </summary>
    public Player? Owner { get; private set; }

    /// <summary>
    /// The square has all lines completed or has an owner.
    /// </summary>
    public bool IsCompleted
    {
        get
        {
            if (Owner != null) return true;

            var top = GetLineOwner(Position.Top) != null;
            var bottom = GetLineOwner(Position.Bottom) != null;
            var left = GetLineOwner(Position.Left) != null;
            var right = GetLineOwner(Position.Right) != null;

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
            throw new AlreadyConnected(position);

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
    /// <returns>True if setting this line made the player the Owner</returns>
    public bool SetLine(Position position, Player player)
    {
        // Check if has neighbor in that position
        if (_neighbors.TryGetValue(position, out var neighbor))
            neighbor.SetLine(position, player);
        else
            _lines[position] = player;

        if (IsCompleted)
            Owner = player;

        return IsCompleted;
    }

    /// <summary>
    /// Gets the owner of a specific line.
    /// </summary>
    /// <param name="position">The position to get the owner from</param>
    /// <returns>The owner player index, returns null if nobody is the owner</returns>
    public Player? GetLineOwner(Position position)
    {
        return _neighbors.TryGetValue(position, out var neighbor)
            ? neighbor.GetLineOwner(position.Invert())
            : _lines[position];
    }
}