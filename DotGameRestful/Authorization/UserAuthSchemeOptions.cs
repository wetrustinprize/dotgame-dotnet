using Microsoft.AspNetCore.Authentication;

namespace DotGameRestful.Authorization;

public class UserAuthSchemeOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "UserAuth";
}