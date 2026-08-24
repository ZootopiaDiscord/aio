using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace ZootopiaAio.Web.Client;

/// <summary>
/// Expands the inline markup used in <c>Content/site.json</c> into HTML.
/// </summary>
/// <remarks>
/// Content text may use <c>&lt;channel&gt;name&lt;/&gt;</c> to link to a channel and
/// <c>&lt;command&gt;text&lt;/&gt;</c> to format a command. Everything else is passed through as
/// HTML, so content is trusted and must not come from user input.
/// </remarks>
public static partial class ContentMarkup
{
    /// <summary>Anchor of the channels section, jumped to by channel references.</summary>
    public const string ChannelsAnchor = "channels";

    /// <summary>
    /// Turns content markup into HTML ready to be rendered.
    /// </summary>
    public static MarkupString ToHtml(string text)
    {
        var html = ChannelTag().Replace(text, $"""<a class="channel" href="#{ChannelsAnchor}">$1</a>""");
        html = CommandTag().Replace(html, """<span class="command">$1</span>""");

        return new MarkupString(html);
    }

    [GeneratedRegex(@"<channel>(.*?)</>", RegexOptions.Singleline)]
    private static partial Regex ChannelTag();

    [GeneratedRegex(@"<command>(.*?)</>", RegexOptions.Singleline)]
    private static partial Regex CommandTag();
}