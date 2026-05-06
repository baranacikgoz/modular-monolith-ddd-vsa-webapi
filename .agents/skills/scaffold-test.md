---
description: Generate a vertical slice integration test for a READ or WRITE feature.
argument-hint: "<Module> <Feature> READ|WRITE"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold test: $ARGUMENTS

1. **Load the factory**: read the module-specific test factory (or `src/Common/Common.Tests/IntegrationTestFactory.cs`) to understand the fixture pattern.

2. **Create test file** at `src/Modules/{Module}/{Module}.Tests/Endpoints/{Feature}Tests.cs`:
   ```csharp
   public class {Feature}Tests : IClassFixture<{Module}TestFactory>
   {
       private readonly HttpClient _client;
       private readonly {Module}TestFactory _factory;

       public {Feature}Tests({Module}TestFactory factory)
       {
           _factory = factory;
           _client = factory.CreateClient();
       }
   }
   ```

3. **WRITE test**:
   - Arrange: build request DTO with Bogus.
   - Act: `await _client.PostAsJsonAsync("/route", request)`.
   - Assert: status code, entity in DB, record in `OutboxMessages`.

4. **READ test**:
   - Arrange: seed entity directly into DB via scoped `DbContext`.
   - Act: `await _client.GetAsync($"/route/{id}")`.
   - Assert: deserialized response matches seeded entity. Use `Assert.Equal`, never FluentAssertions.

5. **Run**: `make test-{module}` and confirm green.
