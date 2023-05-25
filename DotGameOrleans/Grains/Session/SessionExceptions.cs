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
    public SessionNotInitialized(string grainId) : base($"Session with grain id {grainId} is not initialized")
    {
    }
}