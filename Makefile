# Makefile for SonarQube analysis

# Include the .env file
include .env

.PHONY: sonar test

test:
	@echo "Executing unified test suite via run-tests.sh..."
	@bash ./run-tests.sh

sonar:
	@echo "Beginning SonarQube analysis..."
	@dotnet sonarscanner begin /k:"$(SonarProjectName)" /d:sonar.login="$(SonarToken)" /d:sonar.host.url="$(SonarHostUrl)"
	@dotnet build
	@dotnet sonarscanner end /d:sonar.login="$(SonarToken)"
	@echo "Finished SonarQube analysis."
