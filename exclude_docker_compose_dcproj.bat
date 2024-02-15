@echo off

REM Change directory to the directory of the script
cd /d %~dp0

REM Copy ModularMonolith.sln file to temp file
copy "ModularMonolith.sln" "ModularMonolith_temp.sln"

REM Execute dotnet sln command to remove projects with .dcproj extension
dotnet sln "ModularMonolith.sln" remove .\docker-compose.dcproj
