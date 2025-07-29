# Fase base - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase build - SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia somente o arquivo csproj (sem pasta, pois contexto é MyWalletWebAPI)
COPY MyWalletWebAPI.csproj ./

# Restaura dependências
RUN dotnet restore ./MyWalletWebAPI.csproj

# Copia todo o restante do código fonte
COPY . .

# Build
RUN dotnet build ./MyWalletWebAPI.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./MyWalletWebAPI.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final - runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "MyWalletWebAPI.dll"]
