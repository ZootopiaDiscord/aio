using System.Text.Json.Serialization;

namespace ZootopiaAio.Web.Client.Models;

public sealed record SiteContent
{
    public required string LastRevision { get; init; }
    public required string ServerId { get; init; }
    public required IReadOnlyList<RuleItem> Rules { get; init; }
    public required IReadOnlyList<string> OtherRules { get; init; }
    public required IReadOnlyList<RuleItem> PoliceRules { get; init; }
    public required IReadOnlyList<ChannelCategory> ChannelCategories { get; init; }
    public required IReadOnlyList<Role> Roles { get; init; }
    public required IReadOnlyList<Bot> Bots { get; init; }
    public required IReadOnlyList<MinorBot> MinorBots { get; init; }
    public required IReadOnlyList<Moderator> Moderators { get; init; }
}

public sealed record RuleItem
{
    public required string Text { get; init; }
    public IReadOnlyList<string> SubItems { get; init; } = [];
}

[Flags]
[JsonConverter(typeof(JsonStringEnumConverter<ChannelAudience>))]
public enum ChannelAudience
{
    Everyone = 0,
    Verified = 1,
    Nsfw = 2
}

public sealed record ChannelCategory
{
    public required string Name { get; init; }
    public ChannelAudience Audience { get; init; } = ChannelAudience.Everyone;
    public required IReadOnlyList<Channel> Channels { get; init; }
}

public sealed record Channel
{
    public required string Name { get; init; }
    public required string Id { get; init; }
    public required string Description { get; init; }
    public ChannelAudience Audience { get; init; } = ChannelAudience.Everyone;
}

public sealed record Role
{
    public required string Name { get; init; }
    public required string Description { get; init; }

    [JsonIgnore] public string Slug => Name.Replace(" ", string.Empty).Replace("/", string.Empty).ToLowerInvariant();
}

public sealed record Bot
{
    public required string Name { get; init; }
    public string OriginalName { get; init; } = string.Empty;
    public required string Image { get; init; }
    public required IReadOnlyList<string> Purposes { get; init; }
    public required string Description { get; init; }
}

public sealed record MinorBot
{
    public required string Name { get; init; }
    public required string Image { get; init; }
    public required string Description { get; init; }
}

public sealed record Moderator
{
    public required string Name { get; init; }
    public required string DiscordId { get; init; }
}