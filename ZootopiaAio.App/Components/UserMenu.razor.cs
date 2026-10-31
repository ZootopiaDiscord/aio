using System.Security.Claims;
using Microsoft.AspNetCore.Components;

namespace ZootopiaAio.App.Components;

public partial class UserMenu : ComponentBase
{
    private bool _open;
    [EditorRequired] [Parameter] public required ClaimsPrincipal User { get; set; }
}