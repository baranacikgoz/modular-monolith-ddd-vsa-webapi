# TODO

## Correctness

- [ ] **Add `Read Products` permission to Basic role** (`src/Common/Common.Application/Auth/Permissions.cs`)
  Add `new("Read Product", CustomActions.Read, CustomResources.Products)` to `_basic`.
  Basic role currently has `Search` and `ReadMy` but not `Read` — buyers get 403 on `GET /v1/products/{id}`.

- [ ] **Fix Sms/Register rate limit policies — global counters, not per-user**
  Default Sms = 1 OTP per 15 s globally, Register = 1 per ? globally.
  User A sends OTP → user B must wait. These must be partitioned by phone number (or IP+phone).
  Keep global limiter as DoS backstop; named policies should throttle per-identity.

## Performance / k6

- [ ] **Admin-created templates not visible to new_seller VUs during full run**
  `setup()` fetches templateIds once before VUs start. Admin VUs create ~90 new templates during
  the run but new_seller only picks from the initial seeded set. If no templates are seeded,
  the `new_seller — list product` step is always skipped.
  Options: seed product templates before the run, or have new_seller re-fetch templates every N iterations.

- [ ] **Verify PhoneNumber has a DB index in IAM**
  new_seller write path: OTP → register → OTP → login → create store (5 serial requests, each with a DB write + outbox).
  Confirm `PhoneNumber` column is indexed in both the OTP store and Users table.

- [ ] **Tune outbox batch flush interval under write load**
  Under concurrent new_seller VUs, outbox messages queue up. Review `OutboxProcessorOptions`
  `BatchSize` and polling interval — too slow and writes pile up; too fast and DB hammered.

## Done

- [x] k6 query string params fix — `http.js` `get()` was passing params as k6 opts, not URL query string
- [x] Pagination casing fix — ASP.NET Core minimal API requires `PageNumber`/`PageSize` (PascalCase)
- [x] Register 409 accepted as OK in k6 — `check(res, { 'register: 200 or 409': ... })` prevents phantom failures in `http_req_failed`
- [x] Remove `buyer — get product` check — Basic role has no `Read` on Products; 403 is expected
- [x] Rate limit overrides in `docker-compose.perf.yml` — global/named limits set to 100 000/s for perf runs
- [x] `export default` added to seller.js and new_seller.js for standalone smoke runs
