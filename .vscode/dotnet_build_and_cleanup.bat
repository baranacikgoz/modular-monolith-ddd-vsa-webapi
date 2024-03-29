@echo off
REM Save the current directory
set "currentDir=%CD%"

REM Change to the project's root directory (assumed to be the parent of the .vscode directory)
cd /d %~dp0..
echo Building in %CD%

REM Build the solution and save the exit code
dotnet build %1 /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary;ForceNoAlign
set "buildExitCode=%ERRORLEVEL%"

REM Return to the original directory
cd /d %currentDir%

REM Perform the cleanup task or revert actions regardless of the build success
REM E.g. use revert_exclude.bat as the second argument to revert changes to the solution file
call %2

REM Exit with the original build exit code to indicate failure if build failed
exit /b %buildExitCode%
