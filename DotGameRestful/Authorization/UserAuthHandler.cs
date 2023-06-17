using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DotGameRestful.Authorization;

public class UserAuthHandler : AuthenticationHandler<UserAuthSchemeOptions>
{
    public UserAuthHandler(IOptionsMonitor<UserAuthSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            return Task.FromResult(AuthenticateResult.Fail("Header not found."));

        var headers = Request.Headers[HeaderNames.Authorization].ToString();
        var tokenRegex = new Regex(@"Bearer\s(?<guid>[\d|a-f]{8}-(?:[\d|a-f]{4}-){3}[\d|a-f]{12})");
        var match = tokenRegex.Match(headers);

        if (!match.Success)
            return Task.FromResult(AuthenticateResult.Fail("Invalid header format."));

        if (!Guid.TryParse(match.Groups["guid"].Value, out var guid))
            return Task.FromResult(AuthenticateResult.Fail("Invalid guid format."));

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, guid.ToString())
        }));

        return Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(claimsPrincipal, Scheme.Name)
        ));
    }
}