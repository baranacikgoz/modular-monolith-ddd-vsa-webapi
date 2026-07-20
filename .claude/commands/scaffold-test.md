---
description: Generate a vertical slice integration test for a READ or WRITE feature.
argument-hint: "<Module> <Feature> READ|WRITE"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold test: $ARGUMENTS

1. **Load the factory**: read the module-specific test factory (or `src/Common/Common.Tests/IntegrationTestFactory.cs`) to understand the fixture pattern.

2. **Pick the fixture pattern**: does another test class in this module already use `{Module}TestFactory`? If yes, use `ICollectionFixture` + `[Collection("Name")]` (two `IClassFixture<T>` on different classes in the same assembly boot in parallel and corrupt shared static state) — call `factory.CreateClient()` lazily inside each test body, not the constructor. Otherwise use `IClassFixture` with eager `CreateClient()`:
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

   **Config gotcha**: values set via `AddInMemoryCollection` in `IntegrationTestFactory.ConfigureWebHost` only reach runtime `IOptions<T>` resolution — they do NOT affect `configuration.Get<T>()`/`GetValue<T>()` calls made during DI registration (transport selection, conditional `AddHostedService`, etc.). Anything consumed at registration time needs `builder.UseSetting(...)` instead. A test override that "has no effect" is almost always this.

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

5. **Run**: `make test-{module}` and confirm green.
