FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LPMS.API/LPMS.API.csproj", "LPMS.API/"]
COPY ["LPMS.Infrastructure/LPMS.Infrastructure.csproj", "LPMS.Infrastructure/"]
COPY ["LPMS.Application/LPMS.Application.csproj", "LPMS.Application/"]
COPY ["LPMS.Domain/LPMS.Domain.csproj", "LPMS.Domain/"]
COPY ["LPMS.Shared/LPMS.Shared.csproj", "LPMS.Shared/"]
COPY ["LPMS.EmailService/LPMS.EmailService.csproj", "LPMS.EmailService/"]
RUN dotnet restore "LPMS.API/LPMS.API.csproj"
COPY . .
WORKDIR "/src/LPMS.API"
RUN dotnet build "LPMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LPMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LPMS.API.dll"]
