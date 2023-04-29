using DotGameLogic.Enums;

namespace DotGameLogic.Exceptions;

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
    public AlreadyConnected(
        Position tryingToConnect
    ) : base($"Can't connect {tryingToConnect} to a square which {tryingToConnect.Invert()} is already connected") {}
}