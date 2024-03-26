#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Host/Host.csproj", "src/Host/"]
COPY ["src/Common/Common.Options/Common.Options.csproj", "src/Common/Common.Options/"]
COPY ["src/Common/Common.Localization/Common.Localization.csproj", "src/Common/Common.Localization/"]
COPY ["src/Common/Common.Caching/Common.Caching.csproj", "src/Common/Common.Caching/"]
COPY ["src/Common/Common.Core/Common.Core.csproj", "src/Common/Common.Core/"]
COPY ["src/Common/Common.InterModuleRequests/Common.InterModuleRequests.csproj", "src/Common/Common.InterModuleRequests/"]
COPY ["src/Common/Common.EventBus/Common.EventBus.csproj", "src/Common/Common.EventBus/"]
COPY ["src/Modules/IdentityAndAuth/IdentityAndAuth.csproj", "src/Modules/IdentityAndAuth/"]
COPY ["src/Common/Common.Events/Common.Events.csproj", "src/Common/Common.Events/"]
COPY ["src/Modules/Sales/Sales.csproj", "src/Modules/Sales/"]
COPY ["src/Common/Common.Persistence/Common.Persistence.csproj", "src/Common/Common.Persistence/"]
COPY ["src/Modules/Notifications/Notifications.csproj", "src/Modules/Notifications/"]
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
