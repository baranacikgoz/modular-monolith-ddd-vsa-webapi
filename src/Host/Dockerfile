# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj(s) and restore as distinct layers
COPY ["src/Common/Common.Caching/Common.Caching.csproj", "Common/Common.Caching/"]
COPY ["src/Common/Common.Core/Common.Core.csproj", "Common/Common.Core/"]
COPY ["src/Common/Common.Eventbus/Common.Eventbus.csproj", "Common/Common.Eventbus/"]
COPY ["src/Common/Common.DomainEvents/Common.DomainEvents.csproj", "Common/Common.DomainEvents/"]
COPY ["src/Common/Common.Localization/Common.Localization.csproj", "Common/Common.Localization/"]
COPY ["src/Common/Common.Options/Common.Options.csproj", "Common/Common.Options/"]

COPY ["src/Modules/IdentityAndAuth/IdentityAndAuth.csproj", "Modules/IdentityAndAuth/"]
COPY ["src/Modules/Appointments/Appointments.csproj", "Modules/Appointments/"]

COPY ["src/Host/Host.csproj", "src/Host/"]
RUN dotnet restore "src/Host/Host.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/Host"
ARG configuration=Release
RUN dotnet build "Host.csproj" -c $configuration -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Host.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Check if user 'app' exists; if not, create it
RUN id -u app &>/dev/null || useradd -m app

# Set user context to 'app'
USER app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Host.dll"]
