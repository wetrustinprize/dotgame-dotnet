namespace DotGameGraphQL.Session;

public class SessionErrors
{
    public static IError SessionNotFound = ErrorBuilder
        .New()
        .SetMessage("Session ID wat not found")
        .SetCode("session_not_found")
        .Build();
}