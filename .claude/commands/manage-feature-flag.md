---
description: Create, toggle, add a variant for, or apply a feature flag to endpoints.
argument-hint: "<create|apply|toggle|variant> <FeatureName> [EndpointDirectory]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Manage feature flag: $ARGUMENTS

Config: `src/Host/Host/Configurations/featureFlags.json`, section `FeatureManagement`. Constants: `src/Common/Common.Application/FeatureManagement/FeatureFlags.cs`, nested per module (`FeatureFlags.IAM.Captcha`). Never reference a flag by raw string. No per-environment override file exists; one config per deploy.

| Subcommand | Action |
| :-- | :-- |
| `create <Name>` | Add `"{Module}.{Name}": false` to JSON and a `const string` under the module's nested class in `FeatureFlags.cs`. |
| `toggle <Name> <true\|false>` | Set the value in JSON. |
| `variant <Name>` | Add a `variant` block with `VariantA` (enabled) and `VariantB` (disabled) in JSON, plus the constant. |
| `apply <Name> [Dir]` | Add `.RequireFeature(FeatureFlags.{Module}.{Name})` after `.RequireScope(...)` (or after `.WithDescription(...)` when unauthenticated) in each `Endpoint.cs` under `Dir`, or all when omitted. |

Reference: `src/Modules/IAM/IAM.Endpoints/Captcha/VersionNeutral/ClientKey/Get/Endpoint.cs`. Finish with `make build`.
