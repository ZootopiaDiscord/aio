using System.Text.Json;
using ZootopiaAio.Web.Client.Models;
using ZootopiaAio.Web.Client.Services;

namespace ZootopiaAio.Web.Services;

/// <summary>
/// Reads <c>Content/site.json</c> from the app's directory, re-reading it whenever the file changes
/// so the content can be edited in place without a restart.
/// </summary>
public sealed class SiteContentLoader
{
    private readonly Lock _gate = new();
    private readonly string _path = Path.Combine(AppContext.BaseDirectory, "Content", "site.json");

    private SiteContent? _content;
    private DateTime _readAt;

    /// <summary>
    /// The current content, reloaded if the file has been written since it was last read.
    /// </summary>
    /// <exception cref="InvalidOperationException">The file is missing or does not deserialize.</exception>
    public SiteContent Current
    {
        get
        {
            // Missing files report a sentinel timestamp rather than throwing, so Read reports it.
            var writtenAt = File.GetLastWriteTimeUtc(_path);

            lock (_gate)
            {
                if (_content is null || writtenAt != _readAt)
                {
                    _content = Read();
                    _readAt = writtenAt;
                }

                return _content;
            }
        }
    }

    private SiteContent Read()
    {
        if (!File.Exists(_path))
        {
            throw new InvalidOperationException($"Site content not found at '{_path}'.");
        }

        using var stream = File.OpenRead(_path);

        return JsonSerializer.Deserialize(stream, SiteContentJsonContext.Default.SiteContent)
               ?? throw new InvalidOperationException($"Site content at '{_path}' deserialized to null.");
    }
}
