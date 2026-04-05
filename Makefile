# Makefile — single entry point for all developer commands

# Include the .env file (SonarQube vars)
-include .env

# Temporary solution filter that excludes docker-compose.dcproj
SLNF_BUILD = ModularMonolith.Build.slnf
SLNF_TESTS = ModularMonolith.Tests.slnf

.PHONY: build test sonar \
        ef-add-IAM ef-add-Products ef-add-Outbox \
        ef-script-IAM ef-script-Products ef-script-Outbox ef-script-all

# ── Build & Test ──────────────────────────────────────────────────────────────

build:
	@echo "Building all projects (excluding docker-compose.dcproj)..."
	@PROJECTS=$$(find src -name "*.csproj" | awk '{print "\"" $$0 "\""}' | paste -sd "," -); \
	echo '{"solution":{"path":"ModularMonolith.sln","projects":['"$$PROJECTS"']}}' > $(SLNF_BUILD); \
	dotnet build $(SLNF_BUILD); \
	EXIT_CODE=$$?; \
	rm -f $(SLNF_BUILD); \
	exit $$EXIT_CODE

test:
	@echo "=========================================================="
	@echo "🚀 Running Unified Integration & Unit Test Suite"
	@echo "=========================================================="
	@PROJECTS=$$(find src -name "*.Tests.csproj" | awk '{print "\"" $$0 "\""}' | paste -sd "," -); \
	echo '{"solution":{"path":"ModularMonolith.sln","projects":['"$$PROJECTS"']}}' > $(SLNF_TESTS); \
	echo ""; \
	echo "▶️ Testing Solution Filter: ModularMonolith.Tests.slnf"; \
	dotnet test $(SLNF_TESTS) --verbosity quiet; \
	EXIT_CODE=$$?; \
	rm -f $(SLNF_TESTS); \
	if [ $$EXIT_CODE -eq 0 ]; then \
		echo "=========================================================="; \
		echo "✅ All tests completed successfully!"; \
	fi; \
	exit $$EXIT_CODE

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
		--context IAMDbContext

ef-add-Products:
	dotnet ef migrations add $(name) \
		--project src/Modules/Products/Products.Infrastructure \
		--startup-project src/Host/Host \
		--context ProductsDbContext

ef-add-Outbox:
	dotnet ef migrations add $(name) \
		--project src/Modules/Outbox \
		--startup-project src/Host/Host \
		--context OutboxDbContext

# Usage: make ef-script-IAM
# Generates an idempotent SQL script committed under migrations/{Module}/

ef-script-IAM:
	@mkdir -p migrations/IAM
	dotnet ef migrations script $(from) $(to) \
		--project src/Modules/IAM/IAM.Infrastructure \
		--startup-project src/Host/Host \
		--context IAMDbContext \
		--idempotent \
		--output migrations/IAM/$$(date +%Y%m%d%H%M%S)_IAM.sql
	@echo "Script written to migrations/IAM/"

ef-script-Products:
	@mkdir -p migrations/Products
	dotnet ef migrations script $(from) $(to) \
		--project src/Modules/Products/Products.Infrastructure \
		--startup-project src/Host/Host \
		--context ProductsDbContext \
		--idempotent \
		--output migrations/Products/$$(date +%Y%m%d%H%M%S)_Products.sql
	@echo "Script written to migrations/Products/"

ef-script-Outbox:
	@mkdir -p migrations/Outbox
	dotnet ef migrations script $(from) $(to) \
		--project src/Modules/Outbox \
		--startup-project src/Host/Host \
		--context OutboxDbContext \
		--idempotent \
		--output migrations/Outbox/$$(date +%Y%m%d%H%M%S)_Outbox.sql
	@echo "Script written to migrations/Outbox/"

ef-script-all: ef-script-IAM ef-script-Products ef-script-Outbox
	@echo "All migration scripts generated."
