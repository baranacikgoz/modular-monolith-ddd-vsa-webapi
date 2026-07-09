# Full-Text Search

This document is the design-of-record and developer guide for the search feature. It covers the *why* behind every decision, *how* the mechanism works end-to-end (database write side through HTTP query side), and *how* to extend and maintain it. A developer adding search to a new entity, adding a language, or debugging a missing result should be able to do it from this document alone.

> **Status:** Implemented (2026-06-19). The mechanism described here is the live architecture. Language stamping uses decision **D7-B** — an EF `SaveChangesInterceptor` (`ApplySearchLanguageInterceptor`), so the domain stays pure. The options section is bound as **`FullTextSearchOptions`** (the class name). Sections marked _Build_ document what the implementation produced.

---

## TL;DR — what the feature gives you

- **Multilingual content.** Each row is indexed in the language it was authored in. A Turkish description is stemmed with Turkish rules; an English one with English rules — in the same table, same column.
- **Cross-language discovery where it actually matters.** Brand names, model numbers, SKUs, and proper nouns are findable by every locale, because they live in a language-neutral layer of the index.
- **Accent-insensitive.** Typing `kosu` finds `Koşu`; `sukru` finds `Şükrü`. Works on both the stored data and the query.
- **Relevance ranking.** A term matching a product's *name* ranks above one matching deep in its *description*.
- **Cannot drift.** The search vector is a database-generated column. Every insert/update recomputes it automatically. No application code, no triggers, no "forgot to reindex" class of bug.
- **Locale comes from the request,** via the same `Accept-Language` → `CurrentUICulture` mechanism that already drives `IResxLocalizer`. No `?lang=` parameter.

---

## The problem

A naive `to_tsvector('english', ...)` generated column — the starting point this design replaces — has three defects for this project:

1. **One language, baked in.** The config is frozen into the column DDL. Turkish content stemmed by English rules is stemmed wrong, and Turkish stopwords are not removed. The project's content is genuinely multilingual per row.
2. **No accent handling.** Turkish users routinely type ASCII (`cetin`, `sukru`). English-config search never folds `ç→c` or `ş→s`, so ASCII queries miss accented data.
3. **No relevance.** Results were ordered by `CreatedOn`, so a "search" returned newest-matching, not best-matching.

The hard constraint that shapes everything below: **in PostgreSQL full-text search, the index decides the language, not the query.** A `tsvector` stores already-stemmed lexemes. A query's `tsquery` must be produced by a config compatible with how the rows were indexed, or the lexemes will not align. You cannot fix multilingual search by only parameterizing the query — the *index* has to be language-aware too.

---

## Key decisions

### 1. Per-row authored language, not a fixed column language

**Decision:** Each searchable table that contains prose carries a `Language` column holding the PostgreSQL text-search config name (e.g. `turkish_unaccent`). The generated search vector builds the prose portion with that per-row config.

**Why:** Content is authored once, in one language, by whoever created it. Indexing each row in its own language is the only way stemming and stopword removal are correct for that row. Storing translations of the same row in N languages is a different (much larger) feature and is explicitly out of scope — see [Non-goals](#non-goals).

**Tradeoff:** A row is searchable *as prose* only in the language it was authored in. This is acceptable because a query in language X is composed of language-X words, which would not meaningfully match language-Y prose regardless — the words differ. The genuinely cross-language tokens (brands, models, numbers) are handled by decision 2.

---

### 2. Two-layer vector: a universal layer plus a per-language prose layer

**Decision:** The search vector for a prose-bearing entity is the concatenation of two sub-vectors with different configs and different relevance weights:

- **Universal layer (weight `A`)** — short, proper-noun-like fields (Name, Brand, Model, Color, Address) indexed with the language-neutral `simple_unaccent` config.
- **Prose layer (weight `B`)** — free text (Description) indexed with the row's per-language config.

**Why:** Splitting by *what kind of data the field is* — not by row language — is what makes cross-locale discovery work without making prose search sloppy. A brand like `Nike` indexed with `simple` is matchable by every locale. A description is stemmed per language for the users who actually read that language. The weights give relevance ranking for free: name hits outrank description hits.

**Tradeoff:** Queries must issue two `tsquery`s (see [Query path](#query-path)). This is cheap — both use the same GIN index.

---

### 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger)

**Decision:** The vector is a `GENERATED ALWAYS AS (...) STORED` column whose expression calls a SQL wrapper function marked `IMMUTABLE`. The function performs the per-row `lang::regconfig` cast internally.

**Why:** A generated column guarantees the vector is recomputed on every write — the database enforces it, so application code and bulk loads cannot bypass it. This matches the existing platform philosophy ("boundaries enforced by the compiler, not convention"). The wrapper is necessary because a bare `text::regconfig` cast is only *stable*, and PostgreSQL rejects non-immutable expressions in generated columns; wrapping it in a function declared `IMMUTABLE` is the standard idiom.

**Tradeoff:** Declaring the wrapper `IMMUTABLE` is a deliberate convention — it is not *strictly* immutable (renaming or dropping a text-search config would change its result). This is safe because text-search configs are static infrastructure created once in a migration. The alternative — a `BEFORE INSERT/UPDATE` trigger — allows non-immutable functions but adds imperative DB code, can be bypassed by `COPY`, and hides logic from the EF model. We chose the can't-drift guarantee.

---

### 4. Accent folding via custom `*_unaccent` configs

**Decision:** For every config in use we create an accent-folding variant (`simple_unaccent`, `english_unaccent`, `turkish_unaccent`) that runs the `unaccent` dictionary before the stemmer. These variants are used on both the index side and the query side.

**Why:** Turkish users type ASCII constantly. Folding accents symmetrically on both sides means `cetin` matches `Çetin` and vice versa. Doing it as a named config (rather than wrapping every call in `unaccent(...)`) keeps the index expression and the query expression simple and identical.

**Tradeoff:** Requires the `unaccent` extension and a one-time config setup in a migration.

---

### 5. Language resolved from request culture, never from a query parameter

**Decision:** Both the write-side language (what to store in `Language`) and the read-side query config are derived from `CultureInfo.CurrentUICulture`, which the existing `AcceptLanguageHeaderRequestCultureProvider` sets per request (see `Common.Infrastructure/Localization/Setup.cs`). A C# service maps culture → config name; the domain never reads ambient culture.

**Why:** This is the same mechanism that already chooses the `.resx` file for `IResxLocalizer`. One source of truth for "what language is this request," consistent across localization and search. A `?lang=` query parameter was explicitly rejected — it lets a client desync search language from UI language and complicates validation.

**Tradeoff:** A client wanting results in a non-UI language must change `Accept-Language`. Acceptable and consistent.

---

### 6. No language filter on read

**Decision:** Read queries do **not** filter `WHERE Language = currentConfig`. They match against the whole vector with both query layers.

**Why:** Filtering by language would make products authored in another language invisible — wrong for a marketplace. Because the universal layer is language-neutral and the prose layer naturally only matches same-language words, the dual-query approach (below) gives correct results without a filter. The `Language` column is a **write-side input to the vector**, not a read-side filter.

---

## How it works

### One-time setup _(Build — in a migration)_

Per database (it is shared across modules, but each module owns its own migration; the extension and configs are idempotent with `IF NOT EXISTS` / guarded creation):

```sql
CREATE EXTENSION IF NOT EXISTS unaccent;

-- Accent-folding variants. The unaccent dictionary runs BEFORE the stemmer,
-- so accents are folded and the result is then stemmed (or not, for simple).
CREATE TEXT SEARCH CONFIGURATION simple_unaccent  ( COPY = simple );
ALTER  TEXT SEARCH CONFIGURATION simple_unaccent
       ALTER MAPPING FOR hword, hword_part, word WITH unaccent, simple;

CREATE TEXT SEARCH CONFIGURATION english_unaccent ( COPY = english );
ALTER  TEXT SEARCH CONFIGURATION english_unaccent
       ALTER MAPPING FOR hword, hword_part, word WITH unaccent, english_stem;

CREATE TEXT SEARCH CONFIGURATION turkish_unaccent ( COPY = turkish );
ALTER  TEXT SEARCH CONFIGURATION turkish_unaccent
       ALTER MAPPING FOR hword, hword_part, word WITH unaccent, turkish_stem;
```

> **Verify before relying on `turkish`:** `SELECT cfgname FROM pg_ts_config;` on the target Postgres image. Modern Postgres ships the Turkish snowball stemmer, but if a needed language is absent, map that culture to `simple` in configuration (it is still accent- and Turkish-letter-safe; it just does not stem).

Per prose-bearing entity, an immutable wrapper that produces the two-layer vector:

```sql
CREATE OR REPLACE FUNCTION fts_product(lang text, name text, descr text)
RETURNS tsvector LANGUAGE sql IMMUTABLE AS $$
  SELECT setweight(to_tsvector('simple_unaccent', coalesce(name,  '')), 'A')   -- universal layer
      || setweight(to_tsvector(lang::regconfig,   coalesce(descr, '')), 'B');  -- prose layer (per-row language)
$$;
```

The column and index:

```sql
ALTER TABLE "Products"."Products"
  ADD COLUMN "Language" text NOT NULL DEFAULT 'simple_unaccent',
  ADD COLUMN "SearchVector" tsvector
    GENERATED ALWAYS AS (fts_product("Language", "Name", "Description")) STORED;

CREATE INDEX "IX_Products_SearchVector"
  ON "Products"."Products" USING GIN ("SearchVector");
```

### Write path

```
POST /products    Accept-Language: tr-TR
   │
   ▼  RequestLocalizationMiddleware
CurrentUICulture = "tr"                         (validated against SupportedCultures, else DefaultCulture)
   │
   ▼  ISearchLanguageResolver.Resolve()
"tr" → FullTextSearchOptions.CultureToConfig["tr"] = "turkish" → + unaccent → "turkish_unaccent"
   │  (endpoint passes the resolved config string into the domain factory; the domain stays pure)
   ▼
Product.Create(name, description, searchLanguage: "turkish_unaccent")
   │  Language column ← "turkish_unaccent"
   ▼
INSERT → PostgreSQL computes SearchVector via fts_product(...) automatically
```

Worked example — two rows authored in different locales:

| Row | Name | Description | Language |
|-----|------|-------------|----------|
| **T** | `Koşu Ayakkabısı` | `Hafif ve dayanıklı koşu ayakkabısı` | `turkish_unaccent` |
| **E** | `Running Shoes` | `Lightweight durable running shoes` | `english_unaccent` |

Resulting vectors (lexeme:weight):

- **T** → Name via `simple_unaccent` (folded, **not** stemmed): `'ayakkabisi':A 'kosu':A` · Description via `turkish_unaccent` (folded + Turkish stem, `ve` dropped): `'ayakkab':B 'dayanikli':B 'hafif':B 'kosu':B`
- **E** → Name via `simple_unaccent`: `'running':A 'shoes':A` (note: **not** stemmed — stays `running`) · Description via `english_unaccent`: `'durabl':B 'lightweight':B 'run':B 'shoe':B`

The Name layer uses `simple_unaccent` for **every** row regardless of authored language — that is the universal layer. The Description layer is per-row.

### Query path

```
GET /products/search?searchTerm=...    Accept-Language: <locale>
   │
   ▼  CurrentUICulture → ISearchLanguageResolver → userConfig (e.g. "english_unaccent")
build TWO tsqueries from the same term:
   simpleQ = websearch_to_tsquery('simple_unaccent', term)   -- universal layer, ALL locales
   userQ   = websearch_to_tsquery( userConfig,        term)  -- prose layer, user's language stemming
   │
   ▼
WHERE  SearchVector @@ simpleQ  OR  SearchVector @@ userQ
ORDER BY ts_rank(SearchVector, simpleQ || userQ) DESC      -- only when a term is present
```

Why two queries: a single `tsquery` has one config. Querying the `simple`-indexed name layer with `english` would stem `running → run` and miss the stored `running`. The two OR-ed queries let the universal layer match for everyone while the prose layer is stemmed in the user's language.

Worked searches against rows T and E:

- **English user, `searchTerm=running`** — `simpleQ='running'`, `userQ='run'`. **Row E:** name `'running'` matches `simpleQ` *and* description `'run'` matches `userQ` → strong, weight-`A`-boosted hit. **Row T:** neither matches → not returned (correct — a Turkish product is not an English "running" result; nothing was hidden, the words differ).
- **Any user, brand `nike`** (if name/brand contained it) — `simpleQ='nike'` matches the universal layer of any row regardless of authored language → cross-locale discovery.
- **ASCII `kosu`** — `simpleQ` runs through `unaccent` → `'kosu'`, matches Row T's unaccented stored lexeme `'kosu'` → `kosu` finds `Koşu`.

### Ranking

`ts_rank(SearchVector, simpleQ || userQ)` scores a row using the OR-combined query. The default weight map (`{A=1.0, B=0.4, C=0.2, D=0.1}`, tunable per call) makes universal-layer (`A`) matches outrank prose-layer (`B`) matches. Ordering is applied only when a search term is present; with no term, the existing `PaginateAsync` fallback (`CreatedOn DESC`) stands.

---

## Per-entity strategy

Choose layers by the *kind* of data, not by reflex:

| Entity | Fields | Layers | `Language` column? | Read query |
|--------|--------|--------|--------------------|------------|
| **IAM · ApplicationUser** | `FullName` | universal only (`simple_unaccent`) | No | single `tsquery` |
| **Products · ProductTemplate** | `Brand`, `Model`, `Color` | universal only (`simple_unaccent`) | No | single `tsquery` |
| **Products · Product** | `Name` (univ), `Description` (prose) | universal + prose | Yes | dual `tsquery` |
| **Products · Store** | `Name`, `Address` (univ), `Description` (prose) | universal + prose | Yes | dual `tsquery` |

Entities with no prose (names, brands, SKUs) need **no `Language` column** and a **single** `simple_unaccent` query — names are language-neutral and must never be stemmed. The two-layer/dual-query machinery applies only where free text exists.

---

## Configuration

Bound via the options pattern as `FullTextSearchOptions` (validated with a `CustomValidator<T>`, like every other options class):

```jsonc
// src/Host/Host/Configurations/fullTextSearch.json — section name == class name "FullTextSearchOptions"
"FullTextSearchOptions": {
  "DefaultConfig": "simple_unaccent",          // fallback for unknown cultures (already accent-folding)
  "UseUnaccent": true,                         // resolver appends _unaccent to a culture's base config
  "CultureToConfig": {                          // culture → base config name
    "tr": "turkish",
    "en": "english"
  },
  "RankWeights": [ 0.1, 0.2, 0.4, 1.0 ]         // Postgres/Npgsql ts_rank order {D, C, B, A}
}
```

`IndexMethod` (`GIN`), `UniversalConfig` (`simple_unaccent`), `SearchVectorColumn`, and `LanguageColumn` are compile-time `const`s on `FullTextSearchOptions` (schema-side, referenced by EF config and endpoints), not JSON settings.

**What the options legitimately control, and what they do not** — this matters and is a known sharp edge in this codebase:

- **Runtime, real effect:** the culture→config map and rank weights are read at request time by `ISearchLanguageResolver` and the read endpoints. Changing them in `appsettings` takes effect on restart, no migration.
- **Schema, NOT runtime:** column name, index method, the generated-column expression, and which fields feed the vector live in **migrations**. Options may feed the *default* into `OnModelCreating`, but changing such a value still requires a new migration to alter the schema. Editing `appsettings` will not re-index an existing database. The database schema is the source of truth for DDL; options are the source of truth for request-time behavior.

This split is the same registration-time-vs-runtime distinction documented elsewhere for this project — respect it or a config change will silently "have no effect."

---

## Extending and maintaining

### Add search to a new entity _(Build checklist)_

1. **Decide layers** using the [strategy table](#per-entity-strategy). Prose present → universal + prose + `Language` column. No prose → universal only.
2. **Wrapper function:** add `fts_<entity>(...)` (immutable) in the module's migration, mirroring `fts_product`. Universal fields → `simple_unaccent` weight `A`; prose → `lang::regconfig` weight `B` (use `C`/`D` for additional ranked tiers).
3. **Migration:** add `Language` column (only if prose) and the `GENERATED ALWAYS AS (...) STORED` `SearchVector` column, then the `GIN` index. Follow the raw-SQL pattern used in `IAM`'s `MergeNameAndLastNameIntoFullName` migration for generated-column DDL (drop index → drop column → re-add → re-create index, in that order, when altering).
4. **EF configuration:** map `SearchVector` as `NpgsqlTsVector`, read-only — `ValueGeneratedOnAddOrUpdate()` and set after-save behavior to ignore so EF never writes it. Map `Language` as a normal required string. Keep the `HasIndex(...).HasMethod("GIN")` declaration so the model and migration agree. Do **not** use Npgsql's `IsGeneratedTsVectorColumn` helper — it only emits a single static config and cannot express the two-layer/per-row-language expression.
5. **Domain:** the aggregate factory/method accepts the resolved `searchLanguage` string and stores it (prose entities only). Domain stays pure — it does not read `CurrentUICulture`.
6. **Endpoint (write):** resolve the config via `ISearchLanguageResolver` and pass it into the domain call.
7. **Endpoint (read):** build `simpleQ` (+ `userQ` for prose entities), `WHERE` with `@@` OR, order by `ts_rank` when a term is present. Reference config/column names from `FullTextSearchOptions`, never string literals.
8. **Tests:** see [Testing](#testing).

### Add a new language/culture

1. Confirm the Postgres config exists: `SELECT cfgname FROM pg_ts_config;`. If not, either install/create the dictionary or map the culture to `simple` (no stemming) in `CultureToConfig`.
2. Add the accent-folding variant (`<lang>_unaccent`) in a migration if accent folding is wanted for that language.
3. Add the culture to `CultureToConfig` and to the localization `SupportedCultures` (search language must be a subset of supported request cultures).
4. **No re-index needed for existing rows** — they keep their authored language. Only new rows can use the new config.

### Change the vector (weights, fields, or config)

- **Rank weights** are runtime (`RankWeights`) — no migration.
- **Fields fed into the vector, or the expression itself** — this is a schema change. Write a migration that `DROP`s the generated column and index and re-creates them with the new expression. **A generated `STORED` column is recomputed for every existing row on creation, which rewrites the table and takes an `ACCESS EXCLUSIVE` lock.** On a large table this is a maintenance-window operation — plan it, and generate the idempotent SQL script via the Makefile (`make ef-script-<Module>`).
- Always provide a correct `Down` that restores the previous expression (see the IAM merge migration for the pattern).

---

## Gotchas

- **Config mismatch = silent wrong results.** If the index config and query config disagree (e.g. one stems, the other does not), matches fail with no error. This is why config names come from one place (`FullTextSearchOptions`) and never from inline literals.
- **`simple` does not stem.** A `simple`-indexed `running` is the lexeme `running`, not `run`. The universal-layer query must therefore also use `simple_unaccent`, which is exactly why the read path issues two queries.
- **`ILike('%term%')` fallbacks bypass the index.** A leading-wildcard `ILIKE` cannot use the GIN index and forces a sequential scan. Where the old endpoints combine `SearchTerm` with `ILike` filters on the same fields, prefer the FTS path; keep `ILike` only for genuinely different exact-substring filters and be aware of its cost at scale.
- **Turkish dotted/dotless `i`.** Lowercasing `İ`/`I` is collation-dependent. With `unaccent` in the chain most cases fold to ASCII `i`, but be aware when debugging exact-case Turkish matches.
- **Generated-column immutability.** Never put a bare `text::regconfig` cast directly in a generated-column expression — it is only *stable* and Postgres will reject it. Always go through the `IMMUTABLE` wrapper function.

---

## Testing

Integration tests run against real Postgres via Testcontainers (per the project's testing standards — no mocking the database). For each searchable entity assert:

- **Stemming:** a query for an inflected form finds the base form in the row's language (e.g. Turkish-authored row found by a stemmed Turkish query).
- **Accent folding:** ASCII query finds accented data (`kosu` → `Koşu`) and the reverse.
- **Universal cross-locale:** a brand/proper-noun query in one locale finds a row authored in another locale.
- **Prose isolation:** a language-X prose query does not match unrelated language-Y prose.
- **Ranking:** a row matching on the universal (name) layer ranks above a row matching only on the prose (description) layer.
- **Write-side language capture:** creating an entity under a given `Accept-Language` stores the expected `Language` config.

Run per module: `make test-iam`, `make test-products`.

---

## Non-goals

- **Per-row translations / one-row-findable-in-every-language.** Content is authored once in one language. Making the same row searchable as prose in every locale would require storing N translated vectors per row — a separate feature, deliberately not built. Cross-locale discovery is provided only for the language-neutral universal layer (brands, models, names, numbers).
- **Fuzzy / typo-tolerant search** (trigram similarity, `pg_trgm`) — not part of this design; could be layered later as an additional index.
- **Ranking by business signals** (popularity, recency boosting beyond `CreatedOn` fallback) — out of scope.

---

## File map _(Build)_

| Concern | Location |
|---------|----------|
| Options | `Common.Application/Options/FullTextSearchOptions.cs` (+ validator) |
| Culture→config resolver | `Common.Infrastructure/...` `ISearchLanguageResolver` / impl |
| Extension + custom configs + wrapper fns | per-module migrations (`Products`, `IAM`) |
| Generated column + GIN index DDL | per-module migrations |
| EF mapping (read-only vector, `Language`) | each module's `EntityConfiguration` |
| Domain `searchLanguage` capture | prose aggregates (`Product`, `Store`) |
| Write endpoints (resolve + pass language) | feature `Endpoint.cs` |
| Read endpoints (dual query + rank) | `*/Search/Endpoint.cs` (Products, Products/My, Stores, ProductTemplates, IAM Users) |

---

_When this design is implemented or changed, keep this document in sync with the relevant skills under `.claude/`._
