# Contributing

Thank you for helping improve this template. The fastest way to a merged change is to follow the shape of what already exists, so most of this page points at files to copy and checks to run.

## Before you start

1. Read [`CLAUDE.md`](CLAUDE.md). It is the architecture as numbered rules: module boundaries, the platform services you must not re-implement, persistence, endpoints, events, testing and the options pattern. [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md) explains the reasoning behind them.
2. Get the system running with [`docs/getting-started.md`](docs/getting-started.md).
3. Open an issue first for anything larger than a small fix, so the approach can be agreed before you invest time.

## Making a change

1. Fork the repository and branch from `main`.
2. Find the closest exemplar in section 2 of `CLAUDE.md` and copy its shape. Products is the standard module, Inventory shows the advanced patterns.
3. For a bug, write a failing test first, then fix it. Never edit a test to make it pass.
4. Keep the change focused. A refactor and a feature belong in separate pull requests.

### Rules that CI and the build enforce

- **Zero warnings.** Analyzers run on every project and warnings are errors. `make build` must be clean.
- **No cross-module references.** A module project never references another module project. Share contracts through `Common.IntegrationEvents` and `Common.InterModuleRequests`.
- **Architecture tests pass.** They guard frozen domain event payloads, strongly typed id queries, aggregate method signatures and module boundaries. `Host.Tests` checks that every endpoint scope exists in the Keycloak realm.
- **Migrations come with SQL.** Adding an EF migration means committing both the C# migration and the idempotent SQL script under `migrations/{Module}`. `make check-migration-drift` fails otherwise.
- **Tests run against real infrastructure.** Write tests with the existing fixtures: real PostgreSQL through Testcontainers, `Assert.*` only, `Method_Scenario_Expectation` names. A write endpoint test asserts the entity in the database and the row in `OutboxMessages`.

### Adding common things

| You want to | Do this |
| :-- | :-- |
| Add an endpoint | Follow `scaffold-feature`: `Endpoint.cs`, `Request.cs` with its validator inline, `Response.cs`, one registration line in the feature's `Setup.cs`, and `.RequireScope(...)` with a scope that exists in `keycloak/realm-modular-monolith.json` |
| Add a setting | `{Name}Options` class with a validator in `Common.Application/Options`, a JSON file in `src/Host/Host/Configurations`, an entry in `Configurations/Setup.cs`. Say in the pull request which deployed environments need the new field |
| Tell another module something happened | An integration event (`add-integration-event`) |
| Need an answer from another module | An inter-module request (`add-inter-module-request`) |
| Add a feature flag | `manage-feature-flag`, with a constant in `FeatureFlags` |
| Change the schema | `make ef-add-{Module} name=X`, then `make ef-script-{Module} from=A to=B`, commit both |
| Add a module | `scaffold-module` |

The commands are described in [`docs/ai-workflow.md`](docs/ai-workflow.md). You can follow the same steps by hand.

## Check your work

```bash
make build                       # zero warnings
make test-products               # the module you touched
make test-common                 # architecture tests
make test-host                   # permission coverage, health, logging
make check-migration-drift       # if you changed the model
```

`make test` runs everything one project after another. CI builds `Host`, checks migration drift and then runs one job per test project.

## Commits and pull requests

- Use conventional commit prefixes with a scope, as in the history: `feat(outbox): ...`, `fix(host): ...`, `refactor(common): ...`, `test: ...`, `docs: ...`, `chore(deps): ...`.
- Describe what changed and why. Mention the issue it closes.
- Dependency bumps arrive from Renovate. Reviewing and merging them is welcome. Run `update-dependencies` (or the equivalent by hand) when bumping several at once.

## Reporting problems

Open an issue for bugs, missing documentation and proposals. If you find a security problem, do not open a public issue. Follow [`SECURITY.md`](SECURITY.md).

## License

By contributing you agree that your contribution is licensed under the [Apache License 2.0](LICENSE).
