using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using ZootopiaAio.App.Authentication;

namespace ZootopiaAio.App.Components;

public partial class UserMenu : ComponentBase
{
    // Ordered by priority: the first role the user has determines the colour.
    private static readonly RoleStyle[] RoleStyles =
    [
        new(ZootRoles.Zpd, "inset-ring-role-zpd", "text-role-zpd"),
        new(ZootRoles.Booster, "inset-ring-role-hustler", "text-role-hustler"),
        new(ZootRoles.Verified, "inset-ring-role-zootopian", "text-role-zootopian")
    ];

    private bool _open;
    [EditorRequired] [Parameter] public required ClaimsPrincipal User { get; set; }

    private RoleStyle? PrimaryRole => RoleStyles.FirstOrDefault(r => User.IsInRole(r.Role));

    private string RingClass => PrimaryRole?.RingClass ?? "inset-ring-black dark:inset-ring-white";

    private sealed record RoleStyle(string Role, string RingClass, string TextClass);
}