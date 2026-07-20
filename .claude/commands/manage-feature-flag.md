---
description: Create, apply, or manage a feature flag — add to config, wire RequireFeature on endpoint, or toggle/update existing flag.
argument-hint: "<create|apply|toggle|variant> <FeatureName> [EndpointDirectory]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Manage feature flag: $ARGUMENTS

**Step 1 — Locate config file** at `src/Host/Host/Configurations/featureFlags.json`. Parse `FeatureManagement` section.

**Step 2 — Act based on subcommand:**

| Subcommand | Action |
|------------|--------|
| `create <Name>` | Add new boolean flag set to `false` in `featureFlags.json`. Also add a `const string` for it to `src/Common/Common.Application/FeatureManagement/FeatureFlags.cs` (nested under the owning module's static class) — never reference the flag by raw string. There is no per-environment override file (`featureFlags.{Environment}.json` loading is disabled in `Setup.cs` — this repo has no Vault-like secret injection, one config instance per deploy). |
| `toggle <Name> <true/false>` | Update flag value in `featureFlags.json`. |
| `variant <Name>` | Add variant flag skeleton with two variants (VariantA, VariantB) in config. |
| `apply <Name> [EndpointDir]` | Add `.RequireFeature("<Name>")` to endpoint(s) in `[EndpointDir]`. If no dir given, search all `Endpoint.cs` files. |

**Step 3 — Config update format:**

```json
{
  "FeatureManagement": {
    "Products.NewCheckout": false,
    "Notifications.V2Provider": {
      "enabledFor": [
        { "name": "Microsoft.Targeting", "parameters": { "Audience": { "Groups": [], "DefaultRolloutPercentage": 0 } } }
      ]
    },
    "Checkout.Variant": {
      "variant": {
        "variants": [
          { "name": "VariantA", "configuration": { "enabled": true } },
          { "name": "VariantB", "configuration": { "enabled": false } }
        ]
      }
    }
  }
}
```

**Step 4 — Apply to endpoint.** Insert `.RequireFeature(FeatureFlags.{Module}.{Name})` in `MapEndpoint` chain after `.MustHavePermission()` (or after `.WithDescription()` if no permission) — use the typed constant from `FeatureFlags.cs`, never a raw string:

```csharp
group.MapGet("{id}", HandleAsync)
    .WithDescription("...")
    .MustHavePermission(CustomActions.Read, CustomResources.Products)
    .RequireFeature(FeatureFlags.Products.NewCheckout)
    .TransformResultTo<Response>();
```

**Step 5 — Verify**:
```bash
make build
```
