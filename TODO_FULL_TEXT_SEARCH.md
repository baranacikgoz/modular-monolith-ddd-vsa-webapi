# TODO — Multilingual Full-Text Search

**Status:** ✅ DONE (2026-06-20) · **Owner:** baranacikgoz · **Created:** 2026-06-19

> Implemented. Build zero-warning; Products 98/98 + IAM 39/39 green incl. 7 new `MultilingualSearchTests`; arch audit PASS; idempotent SQL committed under `migrations/`. Root-cause fix during impl: duplicate text-search-config CREATE raises `unique_violation (23505)` on `pg_ts_config`, NOT `duplicate_object` — guarded with `IF NOT EXISTS (SELECT 1 FROM pg_ts_config ...)` instead of `EXCEPTION WHEN duplicate_object`.

This is an executable, self-contained task tracker. An agent with **no prior context** should be able to implement the whole feature from this file plus the linked design doc. Check boxes as you go. Do not deviate from decisions without recording why here.

---

## 0. Read first (context)

- **Design-of-record (authoritative, read fully before coding):** [`docs/FULL_TEXT_SEARCH.md`](docs/FULL_TEXT_SEARCH.md). Explains the *why*, the write→query mechanism, the two-layer index, accents, ranking, gotchas, testing.
- **Project rules:** `CLAUDE.md` (architecture, functional pipeline, REPR endpoints, testing standards, Makefile targets). All apply.
- **What this feature replaces:** today each searchable entity has a static `to_tsvector('english', …)` generated column (`FullTextSearch.cs` in IAM + Products) ordered by `CreatedOn`. Defects: single hard-coded language, no accent folding, no relevance ranking, magic-string config drift between column and query.

### Goal in one paragraph
Each row is indexed in the language it was authored in (per-row `Language` column feeding a `GENERATED … STORED` tsvector). The vector has **two layers**: a language-neutral `simple_unaccent` layer over proper-noun fields (name/brand/model/color/address) so brands are findable by every locale, plus a per-row-language stemmed layer over prose (description). Accents fold both sides (`kosu`↔`Koşu`) via custom `*_unaccent` configs. Reads issue two tsqueries (universal + current-culture) OR-ed, ranked by `ts_rank` with weighted layers. Language (write and query) comes from `CultureInfo.CurrentUICulture` — the same `Accept-Language` mechanism that drives `IResxLocalizer`. No `?lang=` param. No language filter on read.

### Decisions locked
- **D1.** Per-row authored language (not fixed column language).
- **D2.** Two-layer vector: universal `simple_unaccent` (weight A) + per-row-language prose (weight B/C).
- **D3.** `GENERATED … STORED` column via EF `HasComputedColumnSql(stored:true)`; per-row-language entities use an `IMMUTABLE` SQL wrapper fn (`fts_product`, `fts_store`). Not triggers.
- **D4.** Accent folding via custom configs `simple_unaccent` / `english_unaccent` / `turkish_unaccent`.
- **D5.** Write + query language from `CurrentUICulture` via `ISearchLanguageResolver`. No query param.
- **D6.** No `WHERE Language = …` filter on read. `Language` is a write-side vector input only.
- **D7 (stamping mechanism) — chosen: B (EF interceptor).** Stamp `Language` on `Added` entities via a `SaveChangesInterceptor` mirroring `ApplyAuditingInterceptor`; domain stays pure. (Alternative A — thread `SearchLanguage` through the create domain event — is also acceptable now that the DB is empty/no event-versioning risk, but B is preferred for purity. If you switch to A, update this file and the doc.)

### Environment fact
- **DB is currently empty.** No data migration, no rewrite-lock concern, no historical-event-versioning concern. Schema changes are free.

### Per-entity strategy
| Entity | File (EntityConfiguration) | Layers | `Language` col | Read query |
|---|---|---|---|---|
| `IAM.ApplicationUser` (`FullName`) | `IAM.Infrastructure/.../EntityConfigurations/Configs.cs` | universal only | No | single tsquery |
| `Products.ProductTemplate` (`Brand`,`Model`,`Color`) | `Products.Infrastructure/.../ProductTemplateConfiguration.cs` | universal only | No | single tsquery |
| `Products.Product` (`Name` univ, `Description` prose) | `…/ProductConfiguration.cs` | universal + prose | **Yes** | dual tsquery |
| `Products.Store` (`Name`,`Address` univ, `Description` prose) | `…/StoreConfiguration.cs` | universal + prose | **Yes** | dual tsquery |

---

## Phase 0 — Pre-flight
- [ ] Verify Turkish config exists: `SELECT cfgname FROM pg_ts_config;`. If `turkish` absent → set `CultureToConfig["tr"] = "simple"` in config and note it here.

## Phase 1 — Options + resolver (`Common`)
- [ ] **New** `src/Common/Common.Application/Options/FullTextSearchOptions.cs`:
  - `FullTextSearchOptions` + `FullTextSearchOptionsValidator : CustomValidator<FullTextSearchOptions>`.
  - Auto-registered by `AddCommonOptions` (`Common.Application/Options/Setup.cs`) because it lives in that assembly and ends in `Options`. **Config section name = `FullTextSearchOptions`** (class name, NOT `FullTextSearch`).
  - Properties: `string DefaultConfig`, `bool UseUnaccent`, `Dictionary<string,string> CultureToConfig`, `float[] RankWeights` (length **4**, order **{D,C,B,A}** — Postgres/Npgsql convention).
  - Consts/expression-bodied: `UniversalConfig => "simple_unaccent"`, `SearchVectorColumn => "SearchVector"`, `LanguageColumn => "Language"`, `IndexMethod => "GIN"`.
  - Validator: `DefaultConfig` not empty; `CultureToConfig` not empty; `RankWeights.Length == 4`.
- [ ] **New** `src/Common/Common.Application/Search/ISearchLanguageResolver.cs` + `SearchLanguageResolver`:
  - `string ResolveConfig()` → `CultureInfo.CurrentUICulture.TwoLetterISOLanguageName` → lookup `CultureToConfig` (fallback `DefaultConfig`) → append `_unaccent` if `UseUnaccent`.
  - `string UniversalConfig => "simple_unaccent"` (or from options).
  - Inject `IOptions<FullTextSearchOptions>`. Register as singleton where `AddCommonResxLocalization` is wired (`Common.Infrastructure/Localization/Setup.cs` or the common DI setup).
- [ ] Build `Common` only — confirm options auto-bind + validate at startup.

## Phase 2 — Stamp `Language` (interceptor, option B)
- [ ] **New** marker `ISearchLocalized { string Language { get; } }` in `Common.Domain` (or appropriate Common domain location).
- [ ] **New** `ApplySearchLanguageInterceptor : SaveChangesInterceptor` next to `ApplyAuditingInterceptor`. On `EntityState.Added` entries implementing `ISearchLocalized`, set `Language = resolver.ResolveConfig()`. Inject `ISearchLanguageResolver`.
- [ ] Register the interceptor alongside the auditing interceptor in `BaseDbContext` wiring.
- [ ] `Product` and `Store` implement `ISearchLocalized` — add `public string Language { get; private set; } = "simple_unaccent";` (private setter set by interceptor; NO domain event).

## Phase 3 — DB infra (extension, configs, wrapper fns) — inside the migrations of Phase 5
Idempotent + database-global (works for shared DB and split-DB). Put in **each** module migration:
- [ ] `CREATE EXTENSION IF NOT EXISTS unaccent;`
- [ ] Create configs `simple_unaccent`, `english_unaccent`, `turkish_unaccent` (COPY base config, `ALTER MAPPING FOR hword, hword_part, word WITH unaccent, <base_stem>`). Text-search configs lack `IF NOT EXISTS` → wrap each in `DO $$ … EXCEPTION WHEN duplicate_object THEN NULL; $$;`.
- [ ] **Products migration only:** `CREATE OR REPLACE FUNCTION` (IMMUTABLE, `lang::regconfig` cast inside):
  - `fts_product(lang text, name text, descr text)` → `setweight(simple_unaccent(name),'A') || setweight(lang(descr),'B')`.
  - `fts_store(lang text, name text, address text, descr text)` → name `A`, address `B`, descr-in-`lang` `C`.
  - (See exact SQL in `docs/FULL_TEXT_SEARCH.md` → *One-time setup*.)

## Phase 4 — EF config + columns
For each entity (table above), in its EntityConfiguration:
- [ ] **Remove** `IsGeneratedTsVectorColumn(...)` and the `"english"`/`"SearchVector"` literals.
- [ ] Map `SearchVector` shadow property `NpgsqlTsVector` with `.HasComputedColumnSql(<expr>, stored: true)`:
  - `Product`: `fts_product("Language","Name","Description")`
  - `Store`: `fts_store("Language","Name","Address","Description")`
  - `ProductTemplate`: `setweight(to_tsvector('simple_unaccent', coalesce("Brand",'')||' '||coalesce("Model",'')||' '||coalesce("Color",'')),'A')`
  - `ApplicationUser`: `setweight(to_tsvector('simple_unaccent', coalesce("FullName",'')),'A')`
- [ ] Keep `HasIndex(FullTextSearchOptions.SearchVectorColumn).HasMethod("GIN")`.
- [ ] `Product`/`Store`: map `Language` as required string, default `'simple_unaccent'`.
- [ ] Reference column/config names from `FullTextSearchOptions` consts — no literals.

## Phase 5 — Migrations
- [ ] `make ef-add-Products name=MultilingualSearch`
- [ ] `make ef-add-IAM name=MultilingualSearch`
- [ ] **Hand-edit each** (EF will not order raw SQL correctly):
  1. Phase-3 raw SQL FIRST (extension, configs, fns).
  2. Add `Language` column (Product/Store) BEFORE the computed column referencing it.
  3. Drop old english generated cols + GIN indexes; add new computed cols + GIN indexes (EF emits these from Phase-4 model).
  4. Provide a correct `Down` (copy pattern from `IAM …/MergeNameAndLastNameIntoFullName.cs`).
- [ ] `make ef-script-Products` and `make ef-script-IAM` → commit idempotent SQL.
- [ ] DB empty → applying is free; no maintenance window needed.

## Phase 6 — Endpoints (5 read endpoints)
Files: `Products/v1/Search/Endpoint.cs`, `Products/v1/My/Search/Endpoint.cs`, `Stores/v1/Search/Endpoint.cs`, `ProductTemplates/v1/Search/Endpoint.cs`, `IAM Users/VersionNeutral/Search/Endpoint.cs`.
- [ ] Inject `ISearchLanguageResolver` (+ `IOptions<FullTextSearchOptions>` for weights).
- [ ] Build queries when `SearchTerm` present:
  - `universalQ = EF.Functions.WebSearchToTsQuery(resolver.UniversalConfig, term)` (all entities)
  - `proseQ = EF.Functions.WebSearchToTsQuery(resolver.ResolveConfig(), term)` (Product/Store only)
- [ ] `WhereIf(term present, …)`: Product/Store → `vec.Matches(universalQ) || vec.Matches(proseQ)`; User/ProductTemplate → `vec.Matches(universalQ)`.
- [ ] Ranking: pass `orderByDescending = term present ? p => vec.Rank(opts.RankWeights, universalQ.Or(proseQ)) : null` to `PaginateAsync` (it falls back to `CreatedOn` when null). `NpgsqlTsQuery.Or` → `||`; `.Rank(float[], query)` → `ts_rank`.
- [ ] Keep `ILike` only where it's a *distinct* exact-substring filter; drop where redundant with FTS (comment its seq-scan cost).
- [ ] Use `EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)` — no literals.

## Phase 7 — Cleanup
- [ ] Delete `src/Modules/IAM/IAM.Infrastructure/Persistence/EntityConfigurations/FullTextSearch.cs`.
- [ ] Delete `src/Modules/Products/Products.Infrastructure/Persistence/EntityConfigurations/FullTextSearch.cs`.

## Phase 8 — Config
- [ ] Add `FullTextSearchOptions` section to `src/Host/Host/appsettings.json` (and any env-specific files):
  ```jsonc
  "FullTextSearchOptions": {
    "DefaultConfig": "simple_unaccent",
    "UseUnaccent": true,
    "CultureToConfig": { "tr": "turkish", "en": "english" },
    "RankWeights": [ 0.1, 0.2, 0.4, 1.0 ]
  }
  ```
- [ ] Ensure `ResxLocalizationOptions.SupportedCultures` includes the cultures in `CultureToConfig`.
- [ ] Mirror minimal config into integration test setup (`IntegrationTestFactory`) so `IOptions<FullTextSearchOptions>` resolves at runtime (per CLAUDE.md: test config reaches runtime IOptions, not registration-time reads).

## Phase 9 — Tests
Integration tests (real Postgres via Testcontainers, xUnit `Assert.*`, no FluentAssertions). Per `make test-iam` / `make test-products`. Assert:
- [ ] Stemming: inflected-form query finds base form in the row's language.
- [ ] Accent fold: ASCII query finds accented data (`kosu`→`Koşu`) and reverse.
- [ ] Universal cross-locale: brand/proper-noun query in one locale finds a row authored in another.
- [ ] Prose isolation: language-X prose query does not match unrelated language-Y prose.
- [ ] Ranking: name-layer match ranks above description-only match.
- [ ] Write-side capture: creating an entity under a given `Accept-Language` stores the expected `Language` config.

## Phase 10 — Gate + sync
- [ ] `make build` — zero warnings.
- [ ] `make test` — all modules green.
- [ ] `/audit-architecture` — no boundary/outbox/localization/magic-string regressions.
- [ ] **Two-toolchain sync (CLAUDE.md contract):** apply any rule/command changes to `GEMINI.md` + `.agents/skills/` identically.
- [ ] Reconcile `docs/FULL_TEXT_SEARCH.md` to the chosen stamping mechanism (D7 = B) and confirm section name `FullTextSearchOptions`. Flip the doc's _Status_ banner to "Implemented" with date.
- [ ] Delete this TODO file (or mark Done) once all boxes checked.

---

## Build order
3 (infra) → 1 → 2 → 4 → 5 → 6 → 7 → 8 → 9 → 10. Infra/configs/fns must exist before the computed columns and endpoints reference them.

## Gotchas (full list in the design doc)
- Config mismatch between index and query = silent wrong results → always source config names from `FullTextSearchOptions`.
- `simple` does NOT stem; the universal-layer query must use `simple_unaccent` → that's why reads issue two queries.
- Never put a bare `text::regconfig` cast in a generated-column expression (only *stable*) — go through the `IMMUTABLE` wrapper fn.
- `RankWeights` array order is `{D,C,B,A}`.
- Npgsql `IsGeneratedTsVectorColumn` cannot express two-layer/per-row config — use `HasComputedColumnSql`.
