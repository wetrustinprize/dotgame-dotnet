namespace DotGameLogic.Exceptions;

[Serializable]
public class BoardException : Exception
{
    public BoardException() {}
    public BoardException(string message) : base(message) {}
    public BoardException(string message, Exception inner) : base(message, inner) {}
}

[Serializable]
public class InvalidBoardSize : BoardException
{
    public InvalidBoardSize(
        int inputtedWidth,
        int inputtedHeight,
        int minimum
    ): base($"Can't make a board with {inputtedWidth} by {inputtedHeight}, minimum is {minimum} by {minimum}") {}
}