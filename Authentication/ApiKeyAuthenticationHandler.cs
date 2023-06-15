using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IConfiguration configuration)
        : base(options, logger, encoder, clock)
    {
        _configuration = configuration;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("ApiKey", out var apiKeyHeaderValues))
        {
            return AuthenticateResult.Fail("API key is missing");
        }

        var apiKey = apiKeyHeaderValues.ToString();
        // var validApiKey = _configuration.GetValue<string>("ApiKey");
        var validApiKey = "test";

        Console.WriteLine($"apiKey: {apiKey}.");
        Console.WriteLine($"validApiKey: {validApiKey}.");

        if (apiKey != validApiKey)
        {
            return AuthenticateResult.Fail("Invalid API key");
        }

        var claims = new[] { new Claim(ClaimTypes.Authentication, apiKey) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
