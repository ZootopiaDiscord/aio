using AspNetCoreExtensions.Keycloak;
using AspNetCoreExtensions.Keycloak.Options;

namespace ZootopiaAio;

internal static class DependencyInjection
{
    private const string KeycloakScheme = "Keycloak";
    private const string ValkeyKeyPrefix = "aio";

    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers the cookie and OpenID Connect handlers, the Valkey-backed session, token and data protection
        /// stores, and the forwarded headers.
        /// </summary>
        public void AddAuthenticationServices(IConfiguration configuration)
        {
            var idp = KeycloakConfiguration.WithSignedJwt(
                configuration.GetRequiredSection(EnvironmentVariables.KeycloakAuthority).Value!,
                configuration.GetRequiredSection(EnvironmentVariables.KeycloakClientId).Value!,
                configuration.GetRequiredSection(EnvironmentVariables.KeycloakCertificate).Value!,
                configuration.GetRequiredSection(EnvironmentVariables.KeycloakPrivateKey).Value!,
                "profile", "roles");

            var valkey = new ValkeyOptions
            {
                Host = configuration.GetRequiredSection(EnvironmentVariables.ValkeyHost).Value!,
                Password = configuration.GetRequiredSection(EnvironmentVariables.ValkeyPassword).Value!,
                KeyPrefix = ValkeyKeyPrefix,
                KeyEncryptionCertificatePath =
                    configuration.GetRequiredSection(EnvironmentVariables.DataProtectionCertificate).Value!,
                KeyEncryptionPrivateKeyPath =
                    configuration.GetRequiredSection(EnvironmentVariables.DataProtectionPrivateKey).Value!
            };

            services.AddKeycloakAuthentication(idp, valkey, x => x.AuthenticationScheme = KeycloakScheme);
            services.AddAuthorization();
        }
    }

    extension(WebApplication app)
    {
        /// <summary>
        /// Maps the sign-in and sign-out endpoints, the client-facing BFF endpoints, and the two endpoints Keycloak
        /// calls server to server.
        /// </summary>
        public void MapAuthenticationEndpoints()
        {
            app.UseKeycloakAuthentication();
            app.MapLoginAndLogout(KeycloakScheme);
            app.MapBffEndpoints(KeycloakScheme);
        }
    }
}