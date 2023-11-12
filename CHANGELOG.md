## 0.5.2 (2023-11-13)

### Fix

- **ExceptionHandlingMiddleware.cs**: Fix "StatusCode cannot be set because the response has already started." error

## 0.5.1 (2023-11-12)

### Refactor

- **Result.cs**: Improve Result class and extensions to be more reliable

## 0.5.0 (2023-11-12)

### Feat

- **EventBus**: Use current mediator as eventbus instead of Masstransit for simplicity for now

### Fix

- **ResultExtensions.cs**: Trying to return results value even if predecessor fails
- Place adding domain event in static Create methods rather than ctors

### Refactor

- **ErrosAndMessages.cs**: Improve memory usage
- Apply CA inside features, allow more decoupled module registration
- Change publics to internal as much as possible
- Move request validators inside request files
- Adjust namespaces
- **modules**: Make modules more decoupled
- **Result-Monad**: Refactor Bind methods to not the same as Map

### Perf

- **LocalizedErrorTranslator.cs**: Switch to FrozenDictionary to have better lookup performance than traditional dictionary

## 0.4.0 (2023-11-08)

### Feat

- Add Rate Limiting feature

### Fix

- **logging.json**: Rename old serilog.json to logging.json

### Refactor

- Remove redundant logging.Development.json
- Solve analyzer warnings/errors

### Perf

- **logging**: Improve logging performance especially in RequestResponseLoggingMiddleware
- **logging**: Improve performance using [LoggerMessage] attribute

## 0.3.0 (2023-11-06)

### Fix

- **Logging**: Fix "Log.Logger" and "builder.Host.Logger" are different bug
- **Host/Configurations**: Fix configuration addition bug for environment specific json files

### Refactor

- Refactor assembly reference names to be concise

## 0.2.0 (2023-11-06)

### Feat

- **eventbus**: Implement eventbus with MassTransit and Outbox Pattern
- **EventBus**: Configuring eventbus, in-progress
- **https-usage**: Disable https usage to run behind reverse proxy

### Fix

- **Dockerfile**: Add Common.Persistence project
- **cd.yml**: Fix dash bug with docker compose command

### Refactor

- **dependencies**: Switch to central package version management via Directory.Packages.props
- **ApplicationUser.cs**: Make AddDomainEvent method private
- Refactor IntegrationEvents project's name to DomainEvents

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
