namespace DotGameLogic.Exceptions;

[Serializable]
public class BoardException : Exception
{
    public BoardException()
    {
    }

    public BoardException(string message) : base(message)
    {
    }

    public BoardException(string message, Exception inner) : base(message, inner)
    {
    }
}

[Serializable]
public class InvalidBoardSize : BoardException
{
    public InvalidBoardSize(
        int inputtedWidth,
        int inputtedHeight,
        int minimum
    ) : base($"Can't make a board with {inputtedWidth} by {inputtedHeight}, minimum is {minimum} by {minimum}")
    {
    }
}

[Serializable]
public class InvalidBoardPositionException : BoardException
{
    public InvalidBoardPositionException(
        int inputtedX,
        int inputtedY,
        int boardHeight,
        int boardWidth
    ) : base($"Can't get square at {inputtedX}, {inputtedY} on a {boardWidth} by {boardHeight} board")
    {
    }
}