# Makefile — single entry point for all developer commands

# Include the .env file (SonarQube vars)
-include .env

# Temporary solution filter that excludes docker-compose.dcproj
SLNF_BUILD = ModularMonolith.Build.slnf
SLNF_TESTS = ModularMonolith.Tests.slnf

.PHONY: build test test-common test-host test-iam test-products test-outbox test-notifications test-backgroundjobs sonar \
        ef-add-IAM ef-add-Products ef-add-Outbox \
        ef-script-IAM ef-script-Products ef-script-Outbox ef-script-all \
        perf perf-smoke perf-down
# ── Build & Test ──────────────────────────────────────────────────────────────

build:
	@echo "Building all projects (excluding docker-compose.dcproj)..."
	@PROJECTS=$$(find src -name "*.csproj" | awk '{print "\"" $$0 "\""}' | paste -sd "," -); \
	echo '{"solution":{"path":"ModularMonolith.sln","projects":['"$$PROJECTS"']}}' > $(SLNF_BUILD); \
	dotnet build $(SLNF_BUILD); \
	EXIT_CODE=$$?; \
	rm -f $(SLNF_BUILD); \
	exit $$EXIT_CODE

test-common:
	@echo "▶️ Testing Common..."
	dotnet test src/Common/Common.Tests/Common.Tests.csproj

test-host:
	@echo "▶️ Testing Host..."
	dotnet test src/Host/Host.Tests/Host.Tests.csproj

test-iam:
	@echo "▶️ Testing IAM..."
	dotnet test src/Modules/IAM/IAM.Tests/IAM.Tests.csproj

test-products:
	@echo "▶️ Testing Products..."
	dotnet test src/Modules/Products/Products.Tests/Products.Tests.csproj

test-outbox:
	@echo "▶️ Testing Outbox..."
	dotnet test src/Modules/Outbox/Outbox.Tests/Outbox.Tests.csproj

test-notifications:
	@echo "▶️ Testing Notifications..."
	dotnet test src/Modules/Notifications/Notifications.Tests/Notifications.Tests.csproj

test-backgroundjobs:
	@echo "▶️ Testing BackgroundJobs..."
	dotnet test src/Modules/BackgroundJobs/BackgroundJobs.Tests/BackgroundJobs.Tests.csproj

test: test-common test-host test-iam test-products test-outbox test-notifications test-backgroundjobs
	@echo "=========================================================="
	@echo "✅ All tests completed successfully!"

sonar:
	@echo "Beginning SonarQube analysis..."
	@dotnet sonarscanner begin /k:"$(SonarProjectName)" /d:sonar.login="$(SonarToken)" /d:sonar.host.url="$(SonarHostUrl)"
	@$(MAKE) build
	@dotnet sonarscanner end /d:sonar.login="$(SonarToken)"
	@echo "Finished SonarQube analysis."

# ── EF Migration Targets ─────────────────────────────────────────────────────
# Usage: make ef-add-IAM name=AddUserPhoneNumber

ef-add-IAM:
	dotnet ef migrations add $(name) \
		--project src/Modules/IAM/IAM.Infrastructure \
		--startup-project src/Host/Host \
		--context IAMDbContext \
		--output-dir Persistence/Migrations \
		--namespace IAM.Infrastructure.Migrations

ef-add-Products:
	dotnet ef migrations add $(name) \
		--project src/Modules/Products/Products.Infrastructure \
		--startup-project src/Host/Host \
		--context ProductsDbContext \
		--output-dir Persistence/Migrations \
		--namespace Products.Infrastructure.Migrations

ef-add-Outbox:
	dotnet ef migrations add $(name) \
		--project src/Modules/Outbox/Outbox \
		--startup-project src/Host/Host \
		--context OutboxDbContext \
		--output-dir Persistence/Migrations \
		--namespace Outbox.Migrations

# Usage: make ef-script-IAM
# Generates an idempotent SQL script committed under migrations/{Module}/

ef-script-IAM:
	@mkdir -p migrations/IAM
	@output_name=$$(echo "$(to)" | sed 's/^[0-9_]*//'); \
	dotnet ef migrations script $(or $(from),0) $(to) \
		--project src/Modules/IAM/IAM.Infrastructure \
		--startup-project src/Host/Host \
		--context IAMDbContext \
		--idempotent \
		--output "migrations/IAM/$$output_name.sql"
	@echo "Script written to migrations/IAM/"

ef-script-Products:
	@mkdir -p migrations/Products
	@output_name=$$(echo "$(to)" | sed 's/^[0-9_]*//'); \
	dotnet ef migrations script $(or $(from),0) $(to) \
		--project src/Modules/Products/Products.Infrastructure \
		--startup-project src/Host/Host \
		--context ProductsDbContext \
		--idempotent \
		--output "migrations/Products/$$output_name.sql"
	@echo "Script written to migrations/Products/"

ef-script-Outbox:
	@mkdir -p migrations/Outbox
	@output_name=$$(echo "$(to)" | sed 's/^[0-9_]*//'); \
	dotnet ef migrations script $(or $(from),0) $(to) \
		--project src/Modules/Outbox/Outbox \
		--startup-project src/Host/Host \
		--context OutboxDbContext \
		--idempotent \
		--output "migrations/Outbox/$$output_name.sql"
	@echo "Script written to migrations/Outbox/"

ef-script-all: ef-script-IAM ef-script-Products ef-script-Outbox
	@echo "All migration scripts generated."

# ── Performance / Load Testing ───────────────────────────────────────────────
# Requires: docker network create local_shared_network  (once, if not already present)

perf:
	@echo "Starting full stack + k6 load test (50 VUs, ~7 min)..."
	docker compose -f docker-compose.yml -f docker-compose.perf.yml up --build --abort-on-container-exit k6

perf-smoke:
	@echo "Running smoke test (1 VU, 30 s) against buyer scenario..."
	docker compose -f docker-compose.yml up -d
	docker run --rm -i \
		--network local_shared_network \
		-v $$(pwd)/k6:/scripts \
		-e BASE_URL=http://mm.host:5001 \
		grafana/k6:latest run --vus 1 --duration 30s /scripts/scenarios/buyer.js

perf-down:
	docker compose -f docker-compose.yml -f docker-compose.perf.yml down
