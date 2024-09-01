# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the solution-level and common project files
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Host/Host.csproj", "src/Host/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.IntegrationEvents/Common.IntegrationEvents.csproj", "src/Common/Common.IntegrationEvents/"]
COPY ["src/Common/Common.InterModuleRequests/Common.InterModuleRequests.csproj", "src/Common/Common.InterModuleRequests/"]

# Restore dependencies
RUN dotnet restore "./src/Host/Host.csproj"

# Copy the entire source code, including the Modules directory
COPY . .

# Set working directory to the Host project and build it
WORKDIR "/src/src/Host"
RUN dotnet build "./Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

# Copy published output to the final image
COPY --from=publish /app/publish .

# Correctly copy the entire Modules directory for plugin support
COPY --from=build /src/src/Modules /app/Modules

# Debugging: Verify the Modules directory
RUN ls -la /app/Modules

# Set the environment variable for MODULES_PATH
ENV MODULES_PATH=/app/Modules

# Run the application
ENTRYPOINT ["dotnet", "Host.dll"]
