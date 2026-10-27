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
| `Dummy` SMS, email, push and captcha providers | `sms.json`, `email.json`, `push.json`, `captcha.json` | Startup validation fails |
| Fixed one-time code `OtpOptions.DummyCode` | `otp.json` | Startup validation fails |
| In-memory-only cache | `caching.json` | Startup validation fails unless `AllowInMemoryOnlyInProduction` is set |
| The checked-in secret protection key (as `EncryptionKey` or inside `PreviousEncryptionKeys`) | `secretProtection.json` | Startup validation fails in every environment except Development |
| Warehouse connections allowed to point at private addresses | `InventoryOptions.AllowPrivateWarehouseAddresses` in `inventory.json` | Startup validation fails when it is `true` |
| The in-memory `Dummy` warehouse provider | Warehouse connections | Refused when a connection is created and again when the gateway is built |
| The development Keycloak client secrets (`backend-api`, `backend-trusted-login`) | `keycloak.json` | Startup validation fails while a secret still contains the realm file's `change-me` marker |
| SignalR without a Redis backplane | `signalR.json` | Startup validation fails unless `RequireRedisBackplaneInProduction` is turned off |
| Pending database migrations | `MigrationGuard` | The app refuses to start |

These are not checked for you, so review them before any shared or public deployment:

- **Keycloak.** Rotate the `backend-api`, `backend-trusted-login` and `ci-integration` client secrets after the first realm import, and inject the real ones through `KeycloakOptions__*`. A realm that was imported from an earlier version of the file does not receive later scopes, policies and roles; add them by hand (the checklist is in `keycloak/README.md`). The `backend-trusted-login` secret is equivalent to every end user's password. Remove or change the seeded users, whose passwords are in the repository. Keep the token endpoint and the admin console on an internal network. Set `RequireHttpsMetadata` to `true` and make `KC_HOSTNAME` match `KeycloakOptions.BaseUrl`. See [`keycloak/README.md`](keycloak/README.md).
- **Default credentials.** Docker Compose uses `postgres/postgres`, `guest/guest` for RabbitMQ, the password `admin` for Redis and `admin/admin` for the Keycloak console. Replace all of them.
- **Secret protection key.** Set `SecretProtectionOptions.EncryptionKey` (base64 of 32 random bytes) from your secret store. It encrypts the warehouse API credentials and webhook secrets at rest, and the password of a store application while it crosses the message bus. Every stored value carries the id of its key. To rotate, move the old key to `PreviousEncryptionKeys`, set a new `EncryptionKey` and let the Inventory sweep (`WarehouseSecretReprotectCron`) rewrite the stored values; drop the old key only afterwards, because a value whose key is in neither place cannot be decrypted. Webhook secrets are generated per warehouse connection, shown once when created or rotated, and never returned again; request logs redact `apiCredential` and `webhookSecret`.
- **Anonymous endpoints.** `POST /v1/stores/applications` creates an identity-provider account after the applicant proved they own the email address (a single-use verification token from the email code flow) but without a captcha check, and is protected by that proof and the `StoreApplication` rate limit policy (per client IP). An unauthenticated caller can use it to burn the attempts of a pending email login token, the same exposure as the email login itself. The warehouse webhook is anonymous and authenticated by an HMAC signature made with the connection's own secret, with a per-connection rate limit. Tune both policies in `rateLimiting.json`. The webhook path stays behind the global per-IP limiter on purpose.
- **Warehouse addresses.** A caller who may create a warehouse connection chooses an address this server will call. The create validator refuses non-https addresses, user info, `localhost` and private, loopback, link-local, shared and reserved IP literals, and the HTTP client resolves the name itself, refuses any non public answer, connects to the address it checked, follows no redirect and uses no proxy. Do not set `AllowPrivateWarehouseAddresses` outside a developer machine. Because the client uses no proxy, a deployment that can reach the internet only through an egress proxy needs another control for this traffic.
- **Object storage.** `objectStorage.json` holds the credentials and buckets for product images, imports and exports. Keep the private bucket private; the public bucket serves images from unsigned URLs.
- **Network trust.** `reverseProxy.json` trusts forwarded headers from `10.0.0.0/8`. Set it to your proxy network. Set `cors.json` `AllowedOrigins` to your real origins.
- **Configuration files.** They are excluded from the published image and must be mounted at deploy time. Do not commit real values. See [`src/Host/Host/Configurations/README.md`](src/Host/Host/Configurations/README.md).
- **Swagger** is served only in the Development environment. The Hangfire dashboard needs the `hangfire:manage` scope.
- **Dependencies** are pinned in `Directory.Packages.props` and updated through Renovate. Transitive pins with security advisories are noted next to the entries.
