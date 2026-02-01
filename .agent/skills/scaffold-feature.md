skill:
  name: "scaffold-feature"
  description: "Scaffolds a new REPR vertical slice (Read or Write) adhering to the Constitution."
  inputs:
    - name: module
      description: "Target module name (e.g., Products)"
    - name: aggregate
      description: "Aggregate name (e.g., Product)"
    - name: feature
      description: "Feature name (e.g., UpdatePrice)"
    - name: type
      description: "READ (Query) or WRITE (Command)"

  instructions: |
    1. **Analyze Context**: Load `src/Modules/{{module}}`.

    2. **Domain Layer (Write Only)**:
       - If {{type}} is WRITE:
         - Ensure method exists in Aggregate `{{aggregate}}.cs`.
         - Ensure Domain Event is defined: `{{feature}}DomainEvent`.
         - Ensure `RaiseEvent` is called in the Aggregate.

    3. **Endpoint Layer (REPR)**:
       - Create `src/Modules/{{module}}/Endpoints/{{aggregate}}/v1/{{feature}}/`.
       - **Request.cs**: `sealed record Request` with `required` props + `CustomValidator`.
       - **Response.cs**: `sealed record Response`.
       - **Endpoint.cs**:
         - Inherit `Endpoint<Request, Result<Response>>`.
         - Inject `I{{module}}DbContext`.
         - **Logic (READ)**: `db.Aggregates.AsNoTracking().Where(...).Select(x => x.ToResponse()).SingleAsync()`
         - **Logic (WRITE)**: `var agg = await db.Aggregates.FindAsync(id); agg.{{feature}}(...); await db.SaveChangesAsync();`

    4. **Register**:
       - Add mapping to `{{module}}ModuleEndpoints.cs`.
