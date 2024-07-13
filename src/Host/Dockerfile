#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["GlobalUsings.cs", "."]
COPY ["src/Host/Host.csproj", "src/Host/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.IntegrationEvents/Common.IntegrationEvents.csproj", "src/Common/Common.IntegrationEvents/"]
COPY ["src/Common/Common.InterModuleRequests/Common.InterModuleRequests.csproj", "src/Common/Common.InterModuleRequests/"]
COPY ["src/Modules/IAM/IAM.Infrastructure/IAM.Infrastructure.csproj", "src/Modules/IAM/IAM.Infrastructure/"]
COPY ["src/Modules/IAM/IAM.Application/IAM.Application.csproj", "src/Modules/IAM/IAM.Application/"]
COPY ["src/Modules/IAM/IAM.Domain/IAM.Domain.csproj", "src/Modules/IAM/IAM.Domain/"]
COPY ["src/Modules/Inventory/Inventory.Infrastructure/Inventory.Infrastructure.csproj", "src/Modules/Inventory/Inventory.Infrastructure/"]
COPY ["src/Modules/Inventory/Inventory.Application/Inventory.Application.csproj", "src/Modules/Inventory/Inventory.Application/"]
COPY ["src/Modules/Inventory/Inventory.Domain/Inventory.Domain.csproj", "src/Modules/Inventory/Inventory.Domain/"]
COPY ["src/Modules/Notifications/Notifications.Infrastructure/Notifications.Infrastructure.csproj", "src/Modules/Notifications/Notifications.Infrastructure/"]
COPY ["src/Modules/Notifications/Notifications.Application/Notifications.Application.csproj", "src/Modules/Notifications/Notifications.Application/"]
COPY ["src/Modules/Notifications/Notifications.Domain/Notifications.Domain.csproj", "src/Modules/Notifications/Notifications.Domain/"]
COPY ["src/Modules/Outbox/Outbox.csproj", "src/Modules/Outbox/"]
RUN dotnet restore "./src/Host/Host.csproj"
COPY . .
WORKDIR "/src/src/Host"
RUN dotnet build "./Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Host.dll"]
