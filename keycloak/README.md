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
| Realm roles | `basic` (mobile end users), `staff` (support / management), `system-admin` (composite, includes `staff`), `store-owner` (granted by the backend when a store is approved) |
| `backend-api` | Resource server + Authorization Services host. Email + password direct grant. Service account with `realm-management` roles `view-realm`, `view-users`, `query-users`, `manage-users` for the Admin REST API. |
| `backend-trusted-login` | Direct grant flow override `trusted-login`: only `Username Validation`, no credential step. The backend calls it after it has verified a one-time code itself. Its secret is equivalent to every end user's password. |
| `ci-integration` | Sample machine caller (client credentials). Authorized through `Service Account Policy`. |
| Resources / scopes | `users`, `sessions`, `devices`, `stores`, `products`, `product-templates`, `stock-levels`, `stock-reservations`, `warehouse-connections`, `hangfire`; scopes named `resource:action`, ownership variants `-own`. |
| Permissions | Scope-based, grouped per resource and audience (`* Self-Service`, `* Catalog`, `* Administration`, `Machine Integration`). Resource server decision strategy `AFFIRMATIVE`. Administration (the `System Admin Role Policy`) holds `stores:view-all`, `stores:approve`, `stores:suspend`, `stores:reinstate`, `stock-levels:view-all`, `stock-levels:update`, `stock-reservations:view-all`, `warehouse-connections:create`, `warehouse-connections:view`, `warehouse-connections:update` and `products:export`. Any authenticated user (`Any Authenticated User Policy`: `basic`, `staff` or `system-admin`) holds `stock-levels:view-own` (which also covers `GET /v1/stock-levels/my/daily-totals`), `stock-reservations:view-own`, `products:import-own` and `warehouse-connections:view-own`. The `Store Owner Role Policy` (the `store-owner` realm role) holds `warehouse-connections:connect` (`Warehouse Connections Store Owner Permission`). The old `stock-levels:view` and `stock-reservations:view` scopes no longer exist. |
| Ownership | Every `-own` permission applies `Any Authenticated User Policy`, because the route decides ownership in its query (the `my` routes of Inventory join its store access copy). `PermissionCoverageTests` rejects an `-own` permission without it. `warehouse-connections:connect` is the one right held by a role instead: the `store-owner` role (granted on store approval) says who may ask, and the store access copy decides which store. The role is also read by the Notifications hub (a group per role). The API keeps the permissions Keycloak granted a token for the token's life, so an owner approved after signing in needs a token issued after the role was assigned (refresh or sign in again). |
| Tokens | Access token 5 min. SSO idle 14 days, SSO max 90 days. Refresh token rotation on, max reuse 1 (one lost-response retry of the same refresh token is tolerated; the next replay makes Keycloak detach the client session and the API revoke the whole user session). |
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

## Signing in through the API

Clients never talk to Keycloak. The API brokers every flow. Which self-service routes exist depends
on `IdentitySchemeOptions.Scheme` (`src/Host/Host/Configurations/identityScheme.json`), a one-time
choice per deployment.

| Scheme | Self-service routes | Staff and admin |
| :--- | :--- | :--- |
| `PhoneNumber` (default) | `POST /otp/login`, `POST /otp/registration`, `POST /tokens`, `POST /users/register/self`, `GET /users/check-registration` | `POST /tokens/email` |
| `Email` | `POST /users/register/self/email` | `POST /tokens/email` |

`POST /otp/email`, `POST /otp/email/verify`, `POST /tokens/email`, `POST /tokens/refresh` and the
session routes exist in both schemes.

**Phone and one-time code** (end users, `PhoneNumber` scheme). In development the SMS provider is
`Dummy` and `OtpOptions.DummyCode` is `123456`.

```bash
curl -s -X POST http://localhost:5001/otp/login -H 'Content-Type: application/json' \
  -d '{"phoneNumber":"901111111111"}'

curl -s -X POST http://localhost:5001/tokens -H 'Content-Type: application/json' \
  -d '{"phoneNumber":"901111111111","otp":"123456","deviceId":"<guid>","clientId":"mobile-app-1"}'
```

**Email, verification and password** (staff, and everyone in the `Email` scheme). The email must be
verified first, and the password is checked by Keycloak.

```bash
curl -s -X POST http://localhost:5001/otp/email -H 'Content-Type: application/json' \
  -d '{"email":"staff@modular-monolith.local"}'

curl -s -X POST http://localhost:5001/otp/email/verify -H 'Content-Type: application/json' \
  -d '{"email":"staff@modular-monolith.local","otp":"123456"}'
# returns { "isRegistered": true, "emailVerificationToken": "..." }

curl -s -X POST http://localhost:5001/tokens/email -H 'Content-Type: application/json' \
  -d '{"email":"staff@modular-monolith.local","password":"Staff-Dev-Password-1","emailVerificationToken":"<token>","deviceId":"<guid>","clientId":"mobile-app-1"}'
```

Both flows return `accessToken`, `accessTokenExpiresAt`, `refreshToken` and `refreshTokenExpiresAt`.
Exchange the refresh token before the access token expires with
`POST /tokens/refresh` and the body `{"refreshToken":"<token>"}`; the response has the same shape.
`GET /tokens/sessions` lists the caller's sessions, and the current one has `isCurrent: true`.
`clientId` must be one of `DevicesOptions.AllowedClientIds`, and every login binds a session to the
`deviceId` and `clientId` pair. The one-time code endpoints are rate limited (see `rateLimiting.json`),
and a code can be used once.

**Replay defence.** Refresh-token rotation allows one retry of the same token. A second replay makes
Keycloak detach the client session; the API then revokes the whole user session and sends the user a
security alert push through the Notifications module.

## Identity audit trail

IAM has no database of its own (Keycloak is the sole identity store), so "which sessions did user X
create on date Y" is answered by Keycloak's own event log, not `AuditLog`. The realm enables
`eventsEnabled` (login/logout/register/refresh/revoke, 30-day `eventsExpiration`) and
`adminEventsEnabled` (every Admin REST write the API itself makes: user create/delete, role
mapping, session revoke). Both log through the default `jboss-logging` listener; query them with
`GET /admin/realms/{realm}/events` and `GET /admin/realms/{realm}/admin-events`, or point
`eventsListeners` at a different listener (e.g. `email`) in a shared environment. `admin-events`
detail payloads are off (`adminEventsDetailsEnabled: false`) since they can carry request bodies.

## Secrets

Client secrets in the JSON are development values. In any shared environment rotate them after
the first import (Admin console → Clients → Credentials, or `PUT /admin/realms/{realm}/clients/{id}`)
and inject them into the API through `KeycloakOptions__*` environment variables. Never ship the
`backend-trusted-login` secret anywhere but the API's vault.

Keep the Keycloak token endpoint reachable only from the API network and the admin console on an
internal network; the `trusted-login` flow is only as safe as that boundary.

## Realms that already exist

Keycloak skips the import when the realm exists, so a realm created from an earlier version of the file does not receive later additions. Bring it in line by hand (admin console or Admin REST API) with whatever the current file has and the live realm lacks:

- the realm role `store-owner` (the API grants it when a store is approved; without it the consumer that grants it throws and the bus retries),
- the policy `Store Owner Role Policy` (type role, the `store-owner` role) and the permission `Warehouse Connections Store Owner Permission` (scope `warehouse-connections:connect`, policy `Store Owner Role Policy`),
- the resources `stock-levels`, `stock-reservations` and `warehouse-connections` with their scopes and permissions: `stock-levels:view-all` and `stock-levels:update` in `Stock Levels Administration Permission`, `stock-levels:view-own` in `Stock Levels Self-Service Permission`, `stock-reservations:create`, `stock-reservations:view-all` and `stock-reservations:update` in `Stock Reservations Administration Permission`, `stock-reservations:view-own` in `Stock Reservations Self-Service Permission`, `warehouse-connections:create`, `:view` and `:update` in `Warehouse Connections Administration Permission`, and `warehouse-connections:view-own` in `Warehouse Connections Self-Service Permission`,
- the scopes `stores:approve`, `stores:suspend`, `stores:reinstate`, `stores:view-all` (in `Stores Administration Permission`), `products:import-own` and `products:export` and the permissions that carry them,
- every scope above in the client's global scope list (`authorizationSettings.scopes`) as well as on its resource, and the removal of the retired `stock-levels:view` and `stock-reservations:view`.

A token issued before the `store-owner` role was assigned does not carry it, so an owner approved earlier refreshes the token or signs in again before `POST /v1/warehouse-connections/my` is allowed.

`PermissionCoverageTests` checks the file against the endpoints, not a live realm, so it cannot tell you what a live realm is missing.

## Editing the realm

1. Change it in the admin console of a fresh dev instance (`docker compose up mm.keycloak`, console at http://localhost:8080, `admin` / `admin`).
2. Export: `docker exec mm.keycloak /opt/keycloak/bin/kc.sh export --realm modular-monolith --users realm_file --file /tmp/realm.json` then `docker cp mm.keycloak:/tmp/realm.json keycloak/realm-modular-monolith.json`.
3. Strip generated ids you do not need, keep the `trusted-login` flow id (referenced by `backend-trusted-login.authenticationFlowBindingOverrides`).
4. Run `make test-host` (`PermissionCoverageTests` proves every endpoint scope exists in the file) and `make test-iam`.

## Production

- `KC_DB=postgres`, `KC_DB_URL`, `KC_DB_USERNAME`, `KC_DB_PASSWORD`, `KC_HOSTNAME`, `KC_PROXY_HEADERS=xforwarded`, `kc.sh start --optimized --import-realm`.
- Issuer stability: the API validates `iss` against `KeycloakOptions.BaseUrl` + realm, and mints tokens through the same URL, so `KC_HOSTNAME` must match `KeycloakOptions.BaseUrl`.
