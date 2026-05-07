## graphify

This project has a graphify knowledge graph at graphify-out/.

**Query operations — run CLI directly, no skill invocation needed:**
- `graphify query "<question>"` — semantic search, broad context
- `graphify search "<term>"` — keyword search
- `graphify path "<A>" "<B>"` — shortest path between two concepts
- `graphify explain "<concept>"` — plain-language node explanation

**Build/update operations — invoke the graphify skill:**
- `/graphify .` — full pipeline rebuild
- `/graphify . --update` — incremental re-extraction of changed files only

Rules:
- ALWAYS run a graphify query before grep, find, or reading raw files
- Before answering content questions, read graphify-out/GRAPH_REPORT.md for god nodes and community structure
- If the graphify MCP server is active, use `query_graph`, `get_node`, `shortest_path` tools instead of CLI
- After modifying code in this session, run `graphify update .` (AST-only for `.cs` files, no API cost)
