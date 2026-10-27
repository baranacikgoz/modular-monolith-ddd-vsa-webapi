#!/usr/bin/env bash
# Measures the repository numbers the site shows and writes them as a script
# the site loads before core.js.
# Usage: scripts/site-stats.sh [output-file]   (default: site/stats.js)
# Needs full git history (commit count) and the tracked graphify-out/GRAPH_REPORT.md.

set -euo pipefail
cd "$(dirname "$0")/.."

out="${1:-site/stats.js}"

fail() { echo "site-stats: $*" >&2; exit 1; }
count_lines() { wc -l | tr -d ' '; }

[ "$(git rev-parse --is-shallow-repository)" = false ] || fail "shallow clone: the commit count needs full history (fetch-depth: 0)"

graph_line=$(grep -m1 -E '[0-9]+ nodes.*[0-9]+ edges.*[0-9]+ communities' graphify-out/GRAPH_REPORT.md) \
  || fail "no node, edge and community counts in graphify-out/GRAPH_REPORT.md"
read -r graph_nodes graph_edges graph_communities < <(
  echo "$graph_line" | sed -E 's/^[^0-9]*([0-9]+) nodes.* ([0-9]+) edges.* ([0-9]+) communities.*/\1 \2 \3/'
)

declare -a keys=(modules projects csFiles tests commits commands graphNodes graphEdges graphCommunities)
declare -a values=(
  "$(git ls-files src/Modules | cut -d/ -f3 | sort -u | count_lines)"
  "$(git ls-files '*.csproj' | count_lines)"
  "$(git ls-files '*.cs' | count_lines)"
  "$(git grep -hE '^[[:space:]]*\[(Fact|Theory)(\(|\]|,)' -- '*.Tests/*.cs' | count_lines)"
  "$(git rev-list --count HEAD)"
  "$(git ls-files '.claude/commands/*.md' | count_lines)"
  "$graph_nodes"
  "$graph_edges"
  "$graph_communities"
)

body=""
for i in "${!keys[@]}"; do
  value=${values[$i]}
  [[ $value =~ ^[0-9]+$ ]] || fail "${keys[$i]} is not a number: '$value'"
  [ "$value" -gt 0 ] || fail "${keys[$i]} measured 0"
  body+="  \"${keys[$i]}\": $value"
  [ "$i" -lt $((${#keys[@]} - 1)) ] && body+=","
  body+=$'\n'
done

printf 'window.MM_STATS = {\n%s};\n' "$body" > "$out"
echo "site-stats: wrote $out"
