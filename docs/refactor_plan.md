# Refactor Plan: Production-Grade CORS Configuration

> **Issue**: #57 — Plan Refactor: CORS
> **Status**: Planning Only (no code changes)
> **Date**: 2026-04-07

---

## 1. Problem Statement

The current CORS implementation in `src/Host/Host/Infrastructure/Setup.cs` is a wide-open default policy:

```csharp
private static IServiceCollection AddCustomCors(this IServiceCollection services)
{
    return services
        .AddCors(options =>
        {
            // Change this in production.
            options.AddDefaultPolicy(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
}
```

### Code Smells / Violations

| # | Smell | Details |
|---|-------|---------|
| 1 | **Security Risk** | `AllowAnyOrigin()` + `AllowAnyMethod()` + `AllowAnyHeader()` is effectively disabling CORS — any domain can call the API with any HTTP method. |
| 2 | **Not Configuration-Driven** | The policy is hardcoded. There is no way to change allowed origins, methods, or headers without recompiling. This violates the project convention where all infrastructure settings are driven by `appsettings.json` via Options classes (e.g., `CachingOptions`, `JwtOptions`). |
| 3 | **No Validator** | Every other Options class has a `CustomValidator<T>` ensuring startup-time validation. CORS has none. |
| 4 | **No Config File** | Every other infrastructure concern has a dedicated `Configurations/*.json` file. CORS has none. |
| 5 | **Missing `IConfiguration` Dependency** | `AddCustomCors` does not accept `IConfiguration`, so it cannot read from config even if a file existed. |

---

## 2. Target Architecture

Follow the exact same pattern used by every other Options class in the codebase:

```
Options class  →  Validator  →  JSON config file  →  Setup registration  →  Consumed at DI
```

---

## 3. Files to Create

### 3.1 `src/Common/Common.Application/Options/CorsOptions.cs`

Create a new Options class following the established convention (see `CachingOptions.cs`, `JwtOptions.cs`):

```csharp
public class CorsOptions
{
    public required string[] AllowedOrigins { get; set; }
    public required string[] AllowedMethods { get; set; }
    public required string[] AllowedHeaders { get; set; }
    public string[] ExposedHeaders { get; set; } = [];
    public bool AllowCredentials { get; set; }
    public int PreflightMaxAgeInSeconds { get; set; } = 600;
}
```

**Design rationale:**
- `AllowedOrigins`: Explicit list of allowed origins (e.g., `["https://app.example.com"]`). No wildcard by default.
- `AllowedMethods`: Explicit list (e.g., `["GET", "POST", "PUT", "DELETE", "PATCH"]`).
- `AllowedHeaders`: Explicit list (e.g., `["Content-Type", "Authorization", "X-Requested-With"]`).
- `ExposedHeaders`: Headers the browser is allowed to read from the response. Empty by default.
- `AllowCredentials`: When `true`, enables `Access-Control-Allow-Credentials`. **Note**: Cannot be combined with `AllowAnyOrigin` — the validator must enforce this.
- `PreflightMaxAgeInSeconds`: Controls `Access-Control-Max-Age` to reduce preflight request frequency. Defaults to 600s (10 min).

### 3.2 `CorsOptionsValidator` (same file, following existing convention)

```csharp
public class CorsOptionsValidator : CustomValidator<CorsOptions>
{
    public CorsOptionsValidator()
    {
        RuleFor(x => x.AllowedOrigins)
            .NotEmpty()
            .WithMessage("AllowedOrigins must contain at least one origin.");

        RuleFor(x => x.AllowedMethods)
            .NotEmpty()
            .WithMessage("AllowedMethods must contain at least one HTTP method.");

        RuleFor(x => x.AllowedHeaders)
            .NotEmpty()
            .WithMessage("AllowedHeaders must contain at least one header.");

        RuleFor(x => x.PreflightMaxAgeInSeconds)
            .GreaterThan(0)
            .WithMessage("PreflightMaxAgeInSeconds must be greater than 0.");

        // AllowCredentials = true cannot be combined with a wildcard "*" origin
        RuleFor(x => x.AllowedOrigins)
            .Must(origins => !origins.Contains("*"))
            .When(x => x.AllowCredentials)
            .WithMessage("AllowedOrigins cannot contain '*' when AllowCredentials is enabled.");
    }
}
```

### 3.3 `src/Host/Host/Configurations/cors.json`

```json
{
  "CorsOptions": {
    "AllowedOrigins": ["*"],
    "AllowedMethods": ["GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS"],
    "AllowedHeaders": ["Content-Type", "Authorization", "X-Requested-With", "Accept", "Origin"],
    "ExposedHeaders": [],
    "AllowCredentials": false,
    "PreflightMaxAgeInSeconds": 600
  }
}
```

> **Note**: The default JSON uses `"*"` for origins to maintain backward compatibility in development. Production deployments MUST override this file (via `cors.Production.json` or environment variables) with explicit origin URLs.

---

## 4. Files to Modify

### 4.1 `src/Host/Host/Infrastructure/Setup.cs`

**Change**: Update `AddCustomCors` to accept `IConfiguration`, resolve `CorsOptions`, and build the CORS policy from the options.

```csharp
private static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
{
    var corsOptions = configuration.GetSection(nameof(CorsOptions)).Get<CorsOptions>()
        ?? throw new InvalidOperationException("CorsOptions configuration section is missing.");

    return services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            if (corsOptions.AllowedOrigins.Contains("*"))
                builder.AllowAnyOrigin();
            else
                builder.WithOrigins(corsOptions.AllowedOrigins);

            if (corsOptions.AllowedMethods.Contains("*"))
                builder.AllowAnyMethod();
            else
                builder.WithMethods(corsOptions.AllowedMethods);

            if (corsOptions.AllowedHeaders.Contains("*"))
                builder.AllowAnyHeader();
            else
                builder.WithHeaders(corsOptions.AllowedHeaders);

            if (corsOptions.ExposedHeaders.Length > 0)
                builder.WithExposedHeaders(corsOptions.ExposedHeaders);

            if (corsOptions.AllowCredentials)
                builder.AllowCredentials();
            else
                builder.DisallowCredentials();

            builder.SetPreflightMaxAge(TimeSpan.FromSeconds(corsOptions.PreflightMaxAgeInSeconds));
        });
    });
}
```

**Also update the call site** from:
```csharp
.AddCustomCors()
```
to:
```csharp
.AddCustomCors(configuration)
```

### 4.2 `src/Host/Host/Configurations/Setup.cs`

**Change**: Register the new `cors.json` configuration file:

```csharp
AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/cors");
```

---

## 5. Summary of Changes

| File | Action | Purpose |
|------|--------|---------|
| `Common.Application/Options/CorsOptions.cs` | **Create** | Options class + FluentValidation validator |
| `Host/Configurations/cors.json` | **Create** | Default CORS configuration for development |
| `Host/Infrastructure/Setup.cs` | **Modify** | Wire `CorsOptions` from config into CORS middleware |
| `Host/Configurations/Setup.cs` | **Modify** | Register `cors.json` in the configuration pipeline |

**Total: 2 new files, 2 modified files.**

---

## 6. Testing Considerations

- The existing auto-discovery in `Common.Application.Options.Setup.AddCommonOptions()` will automatically bind and validate `CorsOptions` at startup (it scans for all `*Options` classes in the assembly).
- A malformed config (e.g., empty `AllowedOrigins`, or `AllowCredentials: true` with `"*"` origin) will cause a startup validation failure — no silent misconfiguration.
- Existing integration tests should continue to pass since the default `cors.json` preserves the current permissive behavior (`"*"` origins, `AllowCredentials: false`).

---

## 7. Non-Goals (Out of Scope)

- **Named CORS policies**: The current system uses only the default policy via `UseCors()`. Supporting named/per-endpoint policies is a future enhancement.
- **Per-module CORS**: Not needed — CORS is a host-level concern, not a module concern.
- **Environment-specific override files**: Creating `cors.Production.json` is left to the deployment team. The infrastructure supports it via the existing `AddJsonFile` pattern.
