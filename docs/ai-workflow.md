# AI-assisted development workflow

This repository is set up so that an AI coding agent can work in it safely. The same material helps a new human teammate, because it is the architecture written down as rules, exemplars and checks. None of it is required to build or run the code, and the code itself is ordinary .NET.

The files are written for [Claude Code](https://claude.com/claude-code), but the ideas carry over to any agent that reads a rules file.

## The four layers

| Layer | Where | What it does |
| :-- | :-- | :-- |
| Rules | [`CLAUDE.md`](../CLAUDE.md) | The numbered rulebook the agent reads first |
| Commands | [`.claude/commands/`](../.claude/commands) | Fifteen repeatable workflows (plan, scaffold, execute, verify) |
| Guards | [`.claude/hooks/`](../.claude/hooks) and the architecture tests | Mechanical checks that catch what a rule cannot guarantee |
| Map | `graphify-out/` | A generated knowledge graph of the code, so the agent finds things without grepping the tree |

## `CLAUDE.md`

The rulebook is organized in numbered sections so commands can cite them instead of repeating them.

| Section | Contents |
| :-- | :-- |
| 0 Discovery | Search order: the knowledge graph first, then its report, then grep |
| 1 Architecture | Module boundaries, the module table with test targets, and the platform services never to re-implement |
| 2 Exemplars | A table of "need" to "file to copy": a write endpoint, a read endpoint, a module, an aggregate with events, a consumer, a test, and one row per advanced pattern (queued job, projection, owner reads through a projection, ledger, dirty-set push, webhook with dedup, reconcile of an unknown outcome, address guard, secret protection and rotation, store status gate, V2 event and more) |
| 3 Functional pipeline | Result chaining rules, the extension inventory (including `Match`) and the two documented exceptions |
| 4 Persistence | Read and write shapes, tagged raw SQL, search indexes, keyed paging, aggregate method rules, row lock, counter upsert, ledger, jsonb list, constraint violations as results, projections, the aggregate root as the gate for its children |
| 5 Endpoints and C# | REPR layout, logging and redaction, localization, typed parameters, the guard on a caller-supplied address, placeholder providers |
| 6 Cross-module and events | Events versus inter-module requests, ordering, read models, dirty-set push, webhook dedup and ordering, unknown-outcome calls, email ownership proof, event versioning, command method order |
| 7 Observability | Telemetry classes, spans, metrics |
| 8 Testing | Frameworks, fixtures, what each endpoint test asserts, how to handle CI-only failures |
| 9 Options pattern | How to add a setting and what to tell the user afterwards |
| 10 Report every issue | Flag bugs and violations with file and line, never silently skip them |
| 11 Make targets | The commands the agent may run |
| 12 Heavy work | The accept, queue, poll shape for work that grows with the caller's data: job row, 202, consumer, bounded memory, tests |

The design intent: an agent should copy the shape of an existing exemplar rather than invent one, and every rule that can be a test is a test (see [Guardrails](../README.md#guardrails)).

## The commands

Run them as slash commands in Claude Code.

| Command | Purpose |
| :-- | :-- |
| `/plan-feature` | Plan a feature: owning module, cross-module contracts, file inventory, data model, tests, telemetry. No code until approved |
| `/scaffold-feature` | Scaffold a vertical slice (endpoint, request with inline validator, response, domain method) and register it in `Setup.cs` |
| `/scaffold-test` | Scaffold integration and unit tests, before implementation for a red baseline or after for coverage |
| `/execute-feature` | Implement an approved feature plan end to end |
| `/scaffold-module` | Scaffold a new module: four projects, DbContext, persistence setup, `IModule`, telemetry, tests, Makefile targets |
| `/add-integration-event` | Declare an integration event, publish it from a domain event handler, scaffold the consumer |
| `/add-inter-module-request` | Declare a request contract, implement the handler in the source module, wire the caller |
| `/manage-migration` | Add a migration, generate the idempotent SQL script, verify with `check-migration-drift` |
| `/manage-feature-flag` | Create, toggle, add a variant for, or apply a feature flag to endpoints |
| `/fix-bug` | Reproduce, diagnose and fix with a red test first, then green |
| `/plan-refactor` | Plan a refactor: current violations, exact file changes, risks. No code until approved |
| `/execute-refactor` | Execute an approved refactor incrementally with zero regressions |
| `/audit-architecture` | Check the codebase against `CLAUDE.md` and report pass, fail or warning per rule with file and line |
| `/verify-feature` | Quality gate: architecture audit, architecture tests, then the module or full test suite |
| `/update-dependencies` | Update NuGet packages through central package management, then build and run the suite |

A typical feature runs `/plan-feature`, then approval, then `/scaffold-test` for a red baseline, `/execute-feature`, and `/verify-feature`.

## Guards

- **After an edit** to a module `.csproj`, `check-csproj-refs.sh` looks for a `ProjectReference` to another module and stops the agent if it finds one.
- **Before a commit**, `pre-commit-guard.sh` scans every module project for cross-module references and blocks `git commit` while any exist.
- **Architecture tests** in `Common.Tests/Architecture`, `Host.Tests` and `Inventory.Tests/Architecture` fail the build for the rules a hook cannot see: frozen domain event payloads, strongly typed id queries, aggregate method signatures, scope coverage, the scope each owner or administration route asks (the `*RouteScopesTests` classes), handler parameter binding, localized texts for every Inventory error key and the single writer of the stock ledger.
- `.claude/settings.json` allows the everyday commands (`git`, `gh`, `make`, `dotnet`, search tools) and denies force pushes, hard resets, forced cleans and recursive deletes of `src` and `.git`.

## The knowledge graph

`graphify-out/` holds a generated graph of the repository: `GRAPH_REPORT.md` lists the main hubs and communities, and a query tool answers questions such as "where does the outbox publish" or "what connects Products to Inventory". It is a build artifact refreshed by tooling, so do not edit it by hand. If you do not use it, ignore the folder.

## The issue-to-pull-request automation

`.github/ISSUE_TEMPLATE` has eight templates (plan feature, implement endpoint, plan refactor, execute refactor, scaffold tests, execute feature, verify feature, update dependencies) and `.github/workflows/claude-ai-dotnet.yml` can start an agent run when an issue gets a label that begins with `agent: `.

Treat this as an example rather than something that works out of the box in a fork. The workflow runs on self-hosted runner scale sets named after the provider and model, and it only reacts to labels applied by the repository owner. A label has the form `agent: <provider>/<model>`, and the runner tag is derived from it. Each template tells the agent which command file in `.claude/commands` to execute and to follow `CLAUDE.md`, so a fork needs its own runners, its own secrets and its own owner check, but not new instructions.

## Adapting it

- Keep `CLAUDE.md` as the single source of rules and make commands cite its section numbers.
- When you add a rule, ask whether a test can enforce it. A rule in prose is followed when the agent remembers it; a rule in a test is followed always.
- When you add a platform service, add it to the table in section 1 so nobody re-implements it, and add an exemplar to section 2.
