using System.Text.Json;
using System.Text.Json.Serialization;
using ZootopiaAio.Web.Client.Models;

namespace ZootopiaAio.Web.Client.Services;

/// <summary>
/// Used by the server to write the content endpoint's response and by the client to read it, so both
/// ends agree on the shape without falling back to reflection.
/// </summary>
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, ReadCommentHandling = JsonCommentHandling.Skip)]
[JsonSerializable(typeof(SiteContent))]
public sealed partial class SiteContentJsonContext : JsonSerializerContext;
