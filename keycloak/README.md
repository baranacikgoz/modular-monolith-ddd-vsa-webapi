# Keycloak realm as code

`realm-modular-monolith.json` is the single source of truth for the `modular-monolith` realm:
roles, clients, the `trusted-login` authentication flow, Authorization Services (resources,
scopes, policies, permissions), the user profile schema and the dev/test seed users.

It is imported by `docker compose` (dev), by `Testcontainers.Keycloak` (IAM and Host tests)
and by the production `--import-realm` startup. Keycloak skips the import when the realm
already exists, so a changed file only lands on a fresh database (dev: `rm -rf .containers/mm.keycloak`).

## Model

| Concept | Value |
| :--- | :--- |
| Realm roles | `basic` (mobile end users), `staff` (support / management), `system-admin` (composite, includes `staff`) |
| `backend-api` | Resource server + Authorization Services host. Email + password direct grant. Service account with `realm-management` roles `view-realm`, `view-users`, `query-users`, `manage-users` for the Admin REST API. |
| `backend-trusted-login` | Direct grant flow override `trusted-login`: only `Username Validation`, no credential step. The backend calls it after it has verified a one-time code itself. Its secret is equivalent to every end user's password. |
| `ci-integration` | Sample machine caller (client credentials). Authorized through `Service Account Policy`. |
| Resources / scopes | `users`, `sessions`, `devices`, `stores`, `products`, `product-templates`, `hangfire`; scopes named `resource:action`, ownership variants `-own`. |
| Permissions | Scope-based, grouped per resource and audience (`* Self-Service`, `* Catalog`, `* Administration`, `Machine Integration`). Resource server decision strategy `AFFIRMATIVE`. |
| Tokens | Access token 5 min. SSO idle 14 days, SSO max 90 days. Refresh token rotation on, max reuse 0 (a replayed refresh token kills the session). |
| User profile | `username`, `email`, `firstName`, `lastName` (required), `phoneNumber`, `phoneNumberVerified`, `birthDate` (admin-edit only). Unmanaged attributes disabled. |

The backend checks `resource#scope` through the token endpoint
(`grant_type=urn:ietf:params:oauth:grant-type:uma-ticket`, `response_mode=decision`) with the
caller's bearer token, so a policy change in Keycloak is effective without a deploy.

## Seed users (dev / test only)

| Username | Role | Credential |
| :--- | :--- | :--- |
| `901111111111` / `admin@modular-monolith.local` | `system-admin` | phone OTP or password `SystemAdmin-Dev-Password-1` |
| `staff@modular-monolith.local` | `staff` | password `Staff-Dev-Password-1` |
| `901111111112` … `901111111116` | `basic` | phone OTP |

## Secrets

Client secrets in the JSON are development values. In any shared environment rotate them after
the first import (Admin console → Clients → Credentials, or `PUT /admin/realms/{realm}/clients/{id}`)
and inject them into the API through `KeycloakOptions__*` environment variables. Never ship the
`backend-trusted-login` secret anywhere but the API's vault.

Keep the Keycloak token endpoint reachable only from the API network and the admin console on an
internal network; the `trusted-login` flow is only as safe as that boundary.

## Editing the realm

1. Change it in the admin console of a fresh dev instance (`docker compose up mm.keycloak`, console at http://localhost:8080, `admin` / `admin`).
2. Export: `docker exec mm.keycloak /opt/keycloak/bin/kc.sh export --realm modular-monolith --users realm_file --file /tmp/realm.json` then `docker cp mm.keycloak:/tmp/realm.json keycloak/realm-modular-monolith.json`.
3. Strip generated ids you do not need, keep the `trusted-login` flow id (referenced by `backend-trusted-login.authenticationFlowBindingOverrides`).
4. Run `make test-host` (`PermissionCoverageTests` proves every endpoint scope exists in the file) and `make test-iam`.

## Production

- `KC_DB=postgres`, `KC_DB_URL`, `KC_DB_USERNAME`, `KC_DB_PASSWORD`, `KC_HOSTNAME`, `KC_PROXY_HEADERS=xforwarded`, `kc.sh start --optimized --import-realm`.
- Issuer stability: the API validates `iss` against `KeycloakOptions.BaseUrl` + realm, and mints tokens through the same URL, so `KC_HOSTNAME` must match `KeycloakOptions.BaseUrl`.
