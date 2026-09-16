using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ZootopiaAio.App.Authentication;

internal sealed class BffAuthenticationStateProvider(HttpClient http) : AuthenticationStateProvider
{
    private const string NameClaimType = "preferred_username";

    private static readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

    /// <summary>
    /// The task rather than its result, so several <c>AuthorizeView</c>s initialising at once share one request.
    /// </summary>
    private Task<AuthenticationState>? _state;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return _state ??= FetchAsync();
    }

    private async Task<AuthenticationState> FetchAsync()
    {
        HttpResponseMessage response;

        try
        {
            response = await http.GetAsync(BffPaths.User);
        }
        catch (HttpRequestException)
        {
            return Anonymous;
        }

        if (!response.IsSuccessStatusCode)
        {
            return Anonymous;
        }

        var claims = await response.Content.ReadFromJsonAsync<UserClaim[]>() ?? [];

        if (claims.Length is 0)
        {
            return Anonymous;
        }

        var identity = new ClaimsIdentity(
            claims.Select(x => new Claim(x.Type, x.Value)),
            "bff",
            NameClaimType,
            ClaimTypes.Role);

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private sealed record UserClaim(string Type, string Value);
}