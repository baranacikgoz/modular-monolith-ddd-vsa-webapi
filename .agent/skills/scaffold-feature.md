skill:
  name: "scaffold-feature"
  description: "Scaffolds a new REPR vertical slice using the Result Monad Pipeline."
  inputs:
    - name: module
    - name: aggregate
    - name: feature
    - name: type
      description: "READ or WRITE"

  instructions: |
    1. **Endpoint Generation**:
       - Create `src/Modules/{{module}}/Endpoints/{{aggregate}}/v1/{{feature}}/Endpoint.cs`.
       - **Template (WRITE)**:
         ```csharp
         internal static class Endpoint
         {
             internal static void MapEndpoint(RouteGroupBuilder group)
             {
                 group.MapPut("{{aggregate}}s/{id}", HandleAsync)
                      .WithDescription("{{feature}} action")
                      .Produces(StatusCodes.Status204NoContent)
                      .TransformResultToNoContentResponse();
             }

             private static async Task<Result> HandleAsync(
                 [AsParameters] Request request,
                 [FromServices] I{{module}}DbContext dbContext,
                 CancellationToken ct)
             {
                 return await dbContext.{{aggregate}}s
                     .TagWith(nameof(HandleAsync), request.Id)
                     .Where(x => x.Id == request.Id)
                     .SingleAsResultAsync(ct)
                     .TapAsync(agg => agg.{{feature}}(request.Data))
                     .TapAsync(_ => dbContext.SaveChangesAsync(ct));
             }
         }
         ```
       - **Template (READ)**:
         ```csharp
         // ... MapGet ... .TransformResultToOkResponse();

         private static async Task<Result<Response>> HandleAsync(...)
         {
             return await dbContext.{{aggregate}}s.AsNoTracking()
                 .TagWith(nameof(HandleAsync), request.Id)
                 .Where(x => x.Id == request.Id)
                 .Select(x => x.ToResponse()) // Manual Map
                 .SingleAsResultAsync(ct);
         }
         ```

    2. **Request/Response**:
       - Create `Request.cs`: `public sealed record Request([FromRoute] Guid Id, [FromBody] Body Body);`
       - Create `Body.cs`: `public sealed record Body(string Prop, ...);`

    3. **Domain Method (If WRITE)**:
       - Ensure `{{aggregate}}` has method `{{feature}}` that calls `RaiseEvent`.
       - Ensure `ApplyEvent` handles the state change.
