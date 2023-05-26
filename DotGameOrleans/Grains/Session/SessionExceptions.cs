namespace DotGameOrleans.Grains.Session;

[Serializable]
[GenerateSerializer]
public class SessionException : Exception
{
    public SessionException()
    {
    }

    public SessionException(string message) : base(message)
    {
    }

    public SessionException(string message, Exception inner) : base(message, inner)
    {
    }
}

[Serializable]
[GenerateSerializer]
public class SessionNotInitialized : SessionException
{
    public SessionNotInitialized(Guid grainId) : base($"Session with grain id {grainId.ToString()} is not initialized")
    {
    }
}