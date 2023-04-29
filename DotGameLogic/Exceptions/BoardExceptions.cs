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
    public InvalidBoardSize() {}
    public InvalidBoardSize(string message) : base(message) {}
    public InvalidBoardSize(string message, Exception inner) : base(message, inner) {}
}