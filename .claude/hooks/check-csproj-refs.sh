#!/usr/bin/env bash
# PostToolUse hook — fires after Edit or Write.
# Reads hook context from stdin (JSON), checks if a module .csproj was
# modified, and greps for illegal cross-module ProjectReferences.

set -euo pipefail

input=$(cat)
fp=$(echo "$input" | python3 -c "
import sys, json
try:
    d = json.load(sys.stdin)
    # PostToolUse wraps input under tool_input
    print(d.get('tool_input', d).get('file_path', ''))
except Exception:
    pass
" 2>/dev/null || echo "")

[[ "$fp" == *.csproj ]] || exit 0
[[ "$fp" == */Modules/* ]] || exit 0

current_base=$(echo "$fp" | grep -oE '/Modules/[^/]+/' | sed 's|/Modules/||;s|/||' | cut -d'.' -f1)

violations=$(grep -i 'ProjectReference' "$fp" 2>/dev/null | grep '/Modules/' | grep -v "/${current_base}\." || true)

if [ -n "$violations" ]; then
    echo "⚠️  CROSS-MODULE REFERENCE in $(basename "$fp") — fix before committing:"
    echo "$violations"
    exit 2
fi
