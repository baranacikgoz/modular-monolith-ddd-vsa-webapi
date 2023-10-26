FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["src/Common/Common.Caching/Common.Caching.csproj", "Common/Common.Caching/"]
COPY ["src/Common/Common.Core/Common.Core.csproj", "Common/Common.Core/"]
COPY ["src/Common/Common.DomainEvents/Common.DomainEvents.csproj", "Common/Common.DomainEvents/"]
COPY ["src/Common/Common.Eventbus/Common.Eventbus.csproj", "Common/Common.Eventbus/"]
COPY ["src/Common/Common.Localization/Common.Localization.csproj", "Common/Common.Localization/"]
COPY ["src/Common/Common.Options/Common.Options.csproj", "Common/Common.Options/"]

COPY ["src/Modules/IdentityAndAuth/IdentityAndAuth.csproj", "Modules/IdentityAndAuth/"]
COPY ["src/Modules/Appointments/Appointments.csproj", "Modules/Appointments/"]

COPY ["src/Host/Host.csproj", "src/Host/"]

RUN dotnet restore "src/Host/Host.csproj"
COPY . .
WORKDIR "/src/src/Host"
RUN dotnet build "Host.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Host.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Host.dll"]
