@echo off

REM Change directory to the directory of the script
cd /d %~dp0

REM Revert changes to the solution file
git checkout -- "ModularMonolith.sln"
