## 0.1.0 (2023-11-01)

### Fix

- **LocalizedErrorTranslator.cs**: Fix duplicate key error for dictionary
- **CurrentUser.cs**: Solve Guid parse bug causing exception thrown

### Refactor

- **image-name**: Refactor image name from host to webapi for clarity
- **all-repository**: Separate Request, RequestHandler, Response, Endpoint etc. to apply a better VSA
- **Configurations/Setup.cs**: Refactor json file add logic to reduce duplicate code
- **DefaultResponsesOperationFilter.cs**: Reduce code duplication by encapsulating the creation of problem responses
- **Error-translation**: Make error translation more central and module independent
- **ExceptionHandlingMiddleware.cs**: Add trace identifier to response title
- **Result.cs**: Refactor Result and Error classes to use static instances
