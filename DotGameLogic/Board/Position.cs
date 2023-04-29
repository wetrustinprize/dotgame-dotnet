namespace DotGameLogic.Board;

public enum Position
{
    Top,
    Bottom,
    Left,
    Right
}

public static class PositionExtensions
{
    /// <summary>
    /// Inverts the position.
    /// </summary>
    /// <param name="position">The position to be inverted</param>
    /// <returns>The inverted position</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the informed position is unknown</exception>
    public static Position Invert(this Position position)
    {
        return position switch
        {
            Position.Top => Position.Bottom,
            Position.Bottom => Position.Top,
            Position.Left => Position.Right,
            Position.Right => Position.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }
}