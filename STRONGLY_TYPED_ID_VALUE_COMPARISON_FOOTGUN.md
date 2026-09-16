# Problem: `entity.Id.Value == rawGuid` compiles fine but throws at runtime, with nothing structural stopping it

## Current behavior

`StronglyTypedIdValueConverter<TStronglyTypedId>` (`src/Common/Common.Infrastructure/Persistence/ValueConverters/StronglyTypedIdValueConverter.cs`)
maps a strongly-typed id to its raw `DefaultIdType` column via `id => id.Value` / `value => new TStronglyTypedId
{ Value = value }`. EF Core translates a **whole-value** comparison against the converted property fine:

```csharp
.Where(m => m.Id == someTypedId)          // translates: SQL compares the raw column
```

But a **member-access-then-compare** on the same property does not translate at all:

```csharp
.Where(m => m.Id.Value == someRawGuid)    // throws InvalidOperationException at query-compile time
```

Both forms compile without warning. `Id` and `Id.Value` are both valid, type-correct property paths, so nothing
at build time distinguishes the safe form from the one that blows up the first time the query actually runs.

## Why this is wrong

This has now cost a real incident in a downstream fork, not a theoretical one. A satıcı (seller) on Tekatlı's
`dev` environment tried to connect a real Trendyol marketplace account and got a 500: `Marketplace.Endpoints
.Connections.v1.My.Create.Endpoint.LoadMarketplaceAsync` compared `m.Id.Value == request.MarketplaceId` (a raw
`Guid` route parameter) instead of wrapping the parameter first and comparing the whole id. A second, still-latent
copy of the identical bug was found in the same module's Search endpoint's optional filter while fixing the first
one.

This is not a one-off typo. The exact same pattern was already hit once before, inside this template itself:
`src/Modules/Products/Products.Infrastructure/InterModuleRequestHandlers/GetProductRequestHandler.cs` carries a
code comment (lines 17-19) explaining the gotcha and the correct form, clearly written after someone got burned by
it there too. A comment in one call site does not stop the next developer, in this repo or any fork, from writing
the same natural-looking `.Id.Value ==` in a new file: it is the obvious thing to reach for when comparing a
strongly-typed id property against a raw scalar (a request DTO field, an inbound API parameter, anything that
hasn't been wrapped in the typed id yet), and nothing catches it until a live query runs against that exact code
path. In the Tekatli case, that path was the "happy path" of a Create endpoint, the one path every other test in
the file skipped because they all seeded state directly in the database and only asserted rejection, so the bug
rode all the way to a real user's dev-environment attempt before anyone saw it.

## Ask

Something structural that fires before a human has to remember a convention or read a comment:

1. **Preferred, matching existing repo convention:** `src/Common/Common.Tests/Architecture/` already hosts
   `ModuleBoundaryTests.cs`, which uses `NetArchTest` for assembly-level rules. `NetArchTest` operates on compiled
   types/members, not source syntax, so it can't see inside a LINQ lambda body. A companion architecture test that
   parses each module's `.cs` files with `Microsoft.CodeAnalysis.CSharp` (already part of the SDK, no new
   dependency) and fails when it finds a member-access chain ending in `.Value` on a property whose declared type
   implements `IStronglyTypedId`, compared with `==`/`!=` inside a lambda, would catch both the Marketplace
   instances above and the pre-existing Products one, and run in every `make test-*` without needing a live
   database.
2. **Heavier alternative:** a small Roslyn analyzer package referenced from `Directory.Build.props` so it applies
   solution-wide (and to every fork) with zero per-project enrollment, flagging the same pattern at build time
   with a code-fix that rewrites `x.Id.Value == y` to `x.Id == new TId(y)`.
3. **Minimum viable, if neither of the above is wanted right now:** move the explanation off the one call site in
   `GetProductRequestHandler.cs` and onto `StronglyTypedIdValueConverter<TStronglyTypedId>` itself, as an XML doc
   comment, so it surfaces via IntelliSense/hover at the point someone is about to write the comparison, not only
   in the one file where it was already caught.

Either of the first two turns this from "hope every developer remembers" into "the build tells you the moment you
write it," which is the only version of this that actually stops it from recurring a third time.
