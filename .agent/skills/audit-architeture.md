skill:
  name: "audit-architecture"
  description: "Checks for illegal cross-module references and direct bus publications."
  inputs: []

  instructions: |
    1. **Check Project References**:
       - Scan all `.csproj` files in `src/Modules`.
       - **FAIL** if a Module csproj references another Module csproj.

    2. **Check Outbox Violations**:
       - Search codebase for `IPublishEndpoint.Publish` inside `Endpoints` or `CommandHandlers`.
       - **FAIL** if found in Write-side Command paths (Must use `RaiseEvent`).
       - **PASS** if found in `IntegrationEventHandler` (Relaying events is allowed).

    3. **Check Controller Usage**:
       - Ensure no classes inherit from `ControllerBase`. verify `FastEndpoints` usage.

    4. **Report**: Output a list of violations.
