@echo off

REM Change directory to the directory of the script
cd /d %~dp0

REM Revert changes by deleting the modified file and renaming the temp file
del "ModularMonolith.sln"
ren "ModularMonolith_temp.sln" "ModularMonolith.sln"
