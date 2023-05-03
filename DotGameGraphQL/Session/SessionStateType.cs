namespace DotGameGraphQL.Types;

[GraphQLName("SessionState")]
public class SessionStateType
{
    public string Username { get; set; } = default!;
}