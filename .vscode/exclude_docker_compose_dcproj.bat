@echo off

REM Change directory to the directory of the script
cd /d %~dp0

REM Execute dotnet sln command to remove projects with .dcproj extension
dotnet sln ..\ModularMonolith.sln remove ..\docker-compose.dcproj
