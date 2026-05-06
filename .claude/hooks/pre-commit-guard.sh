#!/usr/bin/env bash
# PreToolUse hook — intercepts Bash tool calls.
# Blocks git commit if any module .csproj has cross-module ProjectReferences.

set -euo pipefail

input=$(cat)
cmd=$(echo "$input" | python3 -c "
import sys, json
try:
    d = json.load(sys.stdin)
    print(d.get('tool_input', d).get('command', ''))
except Exception:
    pass
" 2>/dev/null || echo "")

[[ "$cmd" == *"git commit"* ]] || exit 0

violations=""
while IFS= read -r csproj; do
    module=$(echo "$csproj" | grep -oE '/Modules/[^/]+/' | sed 's|/Modules/||;s|/||' | cut -d'.' -f1)
    refs=$(grep -i 'ProjectReference' "$csproj" 2>/dev/null | grep '/Modules/' | grep -v "/$module\." || true)
    if [ -n "$refs" ]; then
        violations="${violations}"$'\n'"${csproj}:"$'\n'"${refs}"
    fi
done < <(find src/Modules -name "*.csproj" 2>/dev/null)

if [ -n "$violations" ]; then
    echo "⛔ COMMIT BLOCKED — cross-module references detected:"
    echo "$violations"
    echo ""
    echo "Remove illegal ProjectReferences before committing."
    exit 2
fi

echo "✅ Architecture guard passed."
