#!/usr/bin/env bash
# PostToolUse hook: fires after any Edit or Write call.
# If the modified file is a module .csproj, checks for cross-module ProjectReferences.

set -euo pipefail

fp=$(python3 -c "
import os, json, sys
try:
    d = json.loads(os.environ.get('CLAUDE_TOOL_INPUT', '{}'))
    print(d.get('file_path', ''))
except Exception:
    pass
" 2>/dev/null || echo "")

[[ "$fp" == *.csproj ]] || exit 0
[[ "$fp" == */Modules/* ]] || exit 0

# Base module name: IAM.Infrastructure/IAM.Domain/etc → IAM
current_base=$(echo "$fp" | grep -oE '/Modules/[^/]+/' | sed 's|/Modules/||;s|/||' | cut -d'.' -f1)

violations=$(grep -i 'ProjectReference' "$fp" 2>/dev/null | grep '/Modules/' | grep -v "/${current_base}\." || true)

if [ -n "$violations" ]; then
    echo "⚠️  CROSS-MODULE REFERENCE in $(basename "$fp") — fix before committing:"
    echo "$violations"
    exit 2
fi
