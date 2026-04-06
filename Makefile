# Makefile — single entry point for all developer commands

# Include the .env file (SonarQube vars)
-include .env

# Temporary solution filter that excludes docker-compose.dcproj
SLNF_BUILD = ModularMonolith.Build.slnf
SLNF_TESTS = ModularMonolith.Tests.slnf

.PHONY: build test test-common test-host test-iam test-products test-outbox test-notifications test-backgroundjobs sonar \
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
