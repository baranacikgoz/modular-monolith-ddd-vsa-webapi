---
description: Implement REPR pattern endpoints
---
# Workflow: Implement Endpoint

This workflow handles the actual implementation of the business logic following the strict REPR pattern defined in the Rules.

1. **Define Structure**: Create the native files: `Endpoint.cs`, `Request.cs`, and `Response.cs` within the targeted module and versioned endpoint folder.
2. **Implement Handlers**: Write the FastEndpoints handler referencing the "Creating Endpoints Recipe" in `01-instructions.md`.
3. **Business Logic**: 
    - If logic is complex: Encapsulate strictly within the Domain Aggregate and emit `DomainEvents`.
    - If logic is simple: Execute directly in the endpoint.
4. **Performance Rules**: Ensure absolutely NO mapping libraries are used (do inline manual mapping) and apply `.AsNoTracking()` to all read queries.
5. **Registration**: Update the module's target `Setup.cs` referencing the "Registering Endpoints Recipe" in `01-instructions.md`.
