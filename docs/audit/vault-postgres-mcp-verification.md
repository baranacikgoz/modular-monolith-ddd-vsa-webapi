# System Verification Report: Vault & Postgres MCP
**Issue**: #55  
**Date**: 2026-04-06  
**Executed by**: Claude AI Agent (claude-ai-dotnet)

---

## ✅ Verification Summary

All success criteria met. The production environment capabilities are fully operational.

| Criterion | Status | Details |
|-----------|--------|---------|
| `POSTGRES_URL` injected via Vault Sidecar | ✅ PASS | Variable present in environment |
| `ANTHROPIC_API_KEY` injected via Vault Sidecar | ✅ PASS | Variable present in environment |
| Postgres MCP server active & responding | ✅ PASS | `example-servers/postgres` v0.1.0 |
| Database connectivity established | ✅ PASS | TCP handshake + SQL query executed |
| `claude_test` schema accessible | ✅ PASS | Schema discovered |
| `mcp_test` table queryable | ✅ PASS | 2 rows retrieved |
| "online" message retrieved | ✅ PASS | See Data Retrieval section |

---

## 1. Secret Injection Audit (Vault Sidecar)

Both secrets were successfully injected into the environment by the Vault Sidecar:

```
POSTGRES_URL    : PRESENT ✅  (postgresql://claude_readonly:***@main-db-rw.database.svc.cluster.local:5432/app)
ANTHROPIC_API_KEY: PRESENT ✅
```

**Database Host (no password exposed):**
```
main-db-rw.database.svc.cluster.local:5432
```

---

## 2. MCP Tool Discovery

The Postgres MCP server was found configured in `~/.claude.json`:

```json
{
  "mcpServers": {
    "postgres": {
      "command": "mcp-server-postgres",
      "args": ["$POSTGRES_URL"]
    }
  }
}
```

**MCP Server Handshake (JSON-RPC `initialize` response):**
```json
{
  "result": {
    "protocolVersion": "2024-11-05",
    "capabilities": {
      "resources": {},
      "tools": {}
    },
    "serverInfo": {
      "name": "example-servers/postgres",
      "version": "0.1.0"
    }
  }
}
```

**Available MCP Tools:**

| Tool | Description |
|------|-------------|
| `query` | Run a read-only SQL query |

The postgres MCP server is **active and responding** via the `mcp-server-postgres` binary on stdio JSON-RPC transport.

---

## 3. Database Connectivity

**TCP Connectivity Check:**
```
SUCCESS: Connected to main-db-rw.database.svc.cluster.local:5432
```

**Schema Exploration (`claude_test`):**

Available schemas in the database:
- `claude_test` (application test schema)
- `public` (default schema)

Tables in `claude_test`:

| Table Name | Type |
|------------|------|
| `mcp_test` | BASE TABLE |

Schema of `claude_test.mcp_test`:

| Column | Data Type |
|--------|-----------|
| `id` | integer |
| `message` | text |
| `status` | boolean |

---

## 4. Data Retrieval

**Query executed via postgres MCP `query` tool:**
```sql
SELECT * FROM claude_test.mcp_test LIMIT 10;
```

**Result:**
```json
[
  {
    "id": 1,
    "message": "Claude MCP is online and connected to main-db",
    "status": true
  },
  {
    "id": 2,
    "message": "online",
    "status": true
  }
]
```

**✅ The "online" message was successfully retrieved from `claude_test.mcp_test` (row id=2).**

---

## 5. Conclusion

All four verification tasks completed successfully:

1. **Secret Injection** — Both `POSTGRES_URL` and `ANTHROPIC_API_KEY` are present, confirming Vault Sidecar injection is working correctly.
2. **MCP Tool Discovery** — The postgres MCP server (`example-servers/postgres` v0.1.0) is active, responding via stdio JSON-RPC, and exposes the `query` tool.
3. **Database Connectivity** — TCP connectivity to `main-db-rw.database.svc.cluster.local:5432` is established. The `claude_test` schema and `mcp_test` table are accessible.
4. **Data Retrieval** — The `message` field values were successfully retrieved:
   - `"Claude MCP is online and connected to main-db"` (id=1)
   - `"online"` (id=2)

The production environment Vault + Postgres MCP integration is **fully operational**.

---

## Decisions & Rationale

### Why JSON-RPC over stdio instead of a direct psql client?

The MCP server (`mcp-server-postgres`) was already configured and available in the environment. Rather than installing a separate `psql` client (which was not available in the container), communicating directly with the MCP server via its native JSON-RPC/stdio protocol:

1. **Validates the actual MCP integration** — we confirm the tool that Claude Code will use in production actually works end-to-end, not just that TCP port 5432 is open.
2. **Principle of minimal footprint** — no additional packages needed; uses the binary already injected by the infrastructure team.
3. **Protocol fidelity** — JSON-RPC messages (initialize → tools/list → tools/call) mirror exactly how the Claude Agent SDK calls the MCP tool at runtime.

### Why Node.js for the verification script?

`mcp-server-postgres` is a Node.js binary (`/usr/bin/mcp-server-postgres`), and Node.js v20 was available in the runner. Using `child_process.spawn` gave us direct stdio pipe access to the MCP server without any additional dependencies.

### Why read-only credentials (`claude_readonly`)?

The Vault policy correctly provisions least-privilege credentials. The MCP `query` tool is read-only by design, aligning with the principle of **minimal required permissions** for an AI agent operating on production data.
