---
description: Generate a vertical slice integration test for a READ or WRITE feature.
---

Generate a vertical slice integration test. Ask for module name, feature name, and type (READ or WRITE) if not provided.

1. **Load the factory**: read `src/Common/Common.Tests/IntegrationTestFactory.cs` (or the module-specific child factory) to understand the fixture pattern.

2. **Create test file** at `src/Modules/{Module}/{Module}.Tests/Endpoints/{Feature}Tests.cs`:
   ```csharp
   public class {Feature}Tests : IClassFixture<IntegrationTestFactory>
   {
       private readonly HttpClient _client;
       private readonly IntegrationTestFactory _factory;

       public {Feature}Tests(IntegrationTestFactory factory)
       {
           _factory = factory;
           _client = factory.CreateClient();
       }
   }
   ```

3. **WRITE test**:
   - Arrange: build request DTO with Bogus.
   - Act: `await _client.PostAsJsonAsync("/route", request)`.
   - Assert:
     - Status code is `200`/`201`.
     - Entity exists in DB via `_factory.Services.CreateScope()` → `GetRequiredService<{Module}DbContext>()`.
     - A record exists in `OutboxMessages` with the correct event type JSON.

4. **READ test**:
   - Arrange: seed an entity directly into the DB via a scoped `DbContext`.
   - Act: `await _client.GetAsync($"/route/{id}")`.
   - Assert: deserialized response matches seeded entity. Use `Assert.Equal`, never FluentAssertions.

5. **Run**: `make test-{module}` and confirm the tests pass.
