namespace DotGameLogic.Board.Exceptions;

[Serializable]
public class SquareException : Exception
{
    public SquareException() {}
    public SquareException(string message) : base(message) {}
    public SquareException(string message, Exception inner) : base(message, inner) {}
}

[Serializable]
public class AlreadyConnected : SquareException
{
    public AlreadyConnected() {}
    public AlreadyConnected(string message) : base(message) {}
    public AlreadyConnected(string message, Exception inner) : base(message, inner) {}
}