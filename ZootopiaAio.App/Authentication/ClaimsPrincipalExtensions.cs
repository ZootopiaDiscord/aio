using System.Security.Claims;

namespace ZootopiaAio.App.Authentication;

internal static class ClaimsPrincipalExtensions
{
    extension(ClaimsPrincipal user)
    {
        public string? Username => user.Identity?.Name;
        public string? Picture => user.FindFirst("picture")?.Value;
    }
}