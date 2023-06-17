namespace DotGameMemoryServer.Exceptions;

public class InstanceExceptions : Exception
{
    public InstanceExceptions(string message) : base(message)
    {
    }
}

public class InstanceGameAlreadyStarted : InstanceExceptions
{
    public InstanceGameAlreadyStarted() : base("The game has already started")
    {
    }
}