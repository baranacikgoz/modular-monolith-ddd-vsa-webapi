skill:
  name: "scaffold-test"
  description: "Generates a Vertical Slice Integration Test for a feature."
  inputs:
    - name: module
      description: "Module Name"
    - name: feature
      description: "Feature Name (e.g. CreateProduct)"
    - name: type
      description: "READ or WRITE"

  instructions: |
    1. **Context Load**: Read `src/Modules/{{module}}/Tests` to find the `IntegrationTestWebAppFactory`.

    2. **Generate Test File**:
       - Path: `src/Modules/{{module}}/Tests/Endpoints/{{feature}}Tests.cs`
       - Class: `public class {{feature}}Tests : IClassFixture<IntegrationTestWebAppFactory>`

    3. **Implement Test Logic (WRITE)**:
       - Arrange: Create valid Request DTO using `Bogus`.
       - Act: `client.PostAsJsonAsync("/route", request)`.
       - Assert:
         - Status Code is 200/201.
         - **Side Effect**: Query the DB (`scope.ServiceProvider.GetRequiredService<DbContext>()`) and verify Entity exists.
         - **Outbox**: Verify a record exists in `OutboxMessages` with the correct JSON payload.

    4. **Implement Test Logic (READ)**:
       - Arrange: Seed the DB with an entity.
       - Act: `client.GetAsync("/route/id")`.
       - Assert: Response JSON matches the seeded entity.

    5. **Run**: Execute `dotnet test` targeting this file to verify.
