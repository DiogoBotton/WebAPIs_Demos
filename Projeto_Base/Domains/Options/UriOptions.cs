namespace Domains.Options;

/// <summary>
/// Uri options
/// </summary>
public class UriOptions
{
    /// <summary>
    /// Uri of the current service
    /// </summary>
    public string MyUri { get; set; }

    /// <summary>
    /// Allowed cors uris
    /// </summary>
    public string[] AllowedCorsUris { get; set; }
}
