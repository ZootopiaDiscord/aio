namespace ZootopiaAio;

internal static class EnvironmentVariables
{
    /// <summary>
    /// Keycloak realm URL.
    /// </summary>
    public const string KeycloakAuthority = "KEYCLOAK_AUTHORITY";

    /// <summary>
    /// Keycloak client id.
    /// </summary>
    public const string KeycloakClientId = "KEYCLOAK_CLIENT_ID";

    /// <summary>
    /// PEM certificate published at the JWKS endpoint, which Keycloak fetches to verify the client assertion.
    /// </summary>
    public const string KeycloakCertificate = "KEYCLOAK_CERTIFICATE";

    /// <summary>
    /// PEM private key the client assertion is signed with. EC P-256.
    /// </summary>
    public const string KeycloakPrivateKey = "KEYCLOAK_PRIVATE_KEY";

    /// <summary>
    /// Valkey hostname.
    /// </summary>
    public const string ValkeyHost = "VALKEY_HOST";

    /// <summary>
    /// Valkey password.
    /// </summary>
    public const string ValkeyPassword = "VALKEY_PASSWORD";

    /// <summary>
    /// PEM RSA certificate the data protection key ring is encrypted with.
    /// </summary>
    public const string DataProtectionCertificate = "DATAPROTECTION_CERTIFICATE";

    /// <summary>
    /// PEM RSA private key for <see cref="DataProtectionCertificate" />.
    /// </summary>
    public const string DataProtectionPrivateKey = "DATAPROTECTION_PRIVATE_KEY";

    /// <summary>
    /// Enable dev mode to read development-only http headers.
    /// </summary>
    public const string DevMode = "DEV_MODE";
}