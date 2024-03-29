@echo off

REM Assuming this script is located in the .vscode directory, change to the project root directory
cd /d %~dp0..

REM Revert changes to the solution file
git checkout -- "ModularMonolith.sln"
