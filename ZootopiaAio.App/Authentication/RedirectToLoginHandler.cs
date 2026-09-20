using System.Net;
using Microsoft.AspNetCore.Components;

namespace ZootopiaAio.App.Authentication;

/// <summary>
/// Turns a 401 from the BFF into a sign-in.
/// </summary>
internal sealed class RedirectToLoginHandler(NavigationManager navigation) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is not HttpStatusCode.Unauthorized || IsUserEndpoint(request))
        {
            return response;
        }

        navigation.NavigateTo($"{BffPaths.Login}?returnUrl={Uri.EscapeDataString(CurrentPath())}", true);

        return response;
    }

    private static bool IsUserEndpoint(HttpRequestMessage request)
    {
        return request.RequestUri?.AbsolutePath.EndsWith($"/{BffPaths.User}", StringComparison.Ordinal) is true;
    }

    private string CurrentPath()
    {
        return $"/{navigation.ToBaseRelativePath(navigation.Uri)}";
    }
}