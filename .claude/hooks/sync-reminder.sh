#!/usr/bin/env bash
# Stop hook: reminds to sync GEMINI.md if CLAUDE.md was modified more recently.

if [ -f "CLAUDE.md" ] && [ -f "GEMINI.md" ] && [ "CLAUDE.md" -nt "GEMINI.md" ]; then
    echo ""
    echo "⚠️  Sync reminder: CLAUDE.md is newer than GEMINI.md."
    echo "   If rules changed, update GEMINI.md and .agents/skills/ to match."
fi
