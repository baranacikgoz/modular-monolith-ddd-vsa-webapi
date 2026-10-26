# Security policy

## Reporting a vulnerability

Please do not open a public issue for a security problem. Email the maintainer at **baran-acikgoz@outlook.com** with:

- what the problem is and where it is (file, endpoint or configuration key),
- the steps to reproduce it,
- the impact you expect.

Reports are handled privately until a fix is available. Say in your message whether you would like to be credited.

## Supported versions

This repository is a template and has no release trains. The `main` branch is the supported version. Fixes land there.

## What this repository does and does not protect

The code ships with development placeholders so that it runs on a laptop with one command. They are not production secrets, and some of them are deliberately rejected at startup when `ASPNETCORE_ENVIRONMENT` is `Production`:

| Placeholder | Where | Production behavior |
| :-- | :-- | :-- |
| `Dummy` SMS, email, push, captcha and warehouse providers | `sms.json`, `email.json`, `push.json`, `captcha.json`, `inventory.json` | Startup validation fails |
| Fixed one-time code `OtpOptions.DummyCode` | `otp.json` | Startup validation fails |
| In-memory-only cache | `caching.json` | Startup validation fails unless `AllowInMemoryOnlyInProduction` is set |
| SignalR without a Redis backplane | `signalR.json` | Startup validation fails unless `RequireRedisBackplaneInProduction` is turned off |
| Pending database migrations | `MigrationGuard` | The app refuses to start |

These are not checked for you, so review them before any shared or public deployment:

- **Keycloak.** Rotate the `backend-api`, `backend-trusted-login` and `ci-integration` client secrets after the first realm import, and inject the real ones through `KeycloakOptions__*`. The `backend-trusted-login` secret is equivalent to every end user's password. Remove or change the seeded users, whose passwords are in the repository. Keep the token endpoint and the admin console on an internal network. Set `RequireHttpsMetadata` to `true` and make `KC_HOSTNAME` match `KeycloakOptions.BaseUrl`. See [`keycloak/README.md`](keycloak/README.md).
- **Default credentials.** Docker Compose uses `postgres/postgres`, `guest/guest` for RabbitMQ, the password `admin` for Redis and `admin/admin` for the Keycloak console. Replace all of them.
- **Webhook secret.** Replace `InventoryOptions.WebhookSharedSecret`.
- **Network trust.** `reverseProxy.json` trusts forwarded headers from `10.0.0.0/8`. Set it to your proxy network. Set `cors.json` `AllowedOrigins` to your real origins.
- **Configuration files.** They are excluded from the published image and must be mounted at deploy time. Do not commit real values. See [`src/Host/Host/Configurations/README.md`](src/Host/Host/Configurations/README.md).
- **Swagger** is served only in the Development environment. The Hangfire dashboard needs the `hangfire:manage` scope.
- **Dependencies** are pinned in `Directory.Packages.props` and updated through Renovate. Transitive pins with security advisories are noted next to the entries.
