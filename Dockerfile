# Faza budowania: użycie obrazu SDK .NET
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Ustawienie katalogu roboczego
WORKDIR /source

# Kopiowanie plików projektu do kontenera
COPY . .

# Przygotowanie i budowanie projektu
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Faza uruchamiania: użycie obrazu runtime .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Ustawienie katalogu roboczego
WORKDIR /app

# Skopiowanie opublikowanych plików z poprzedniego etapu
COPY --from=build /app .

# Wystawienie portu aplikacji
EXPOSE 5001

# Uruchomienie aplikacji
ENTRYPOINT ["dotnet", "MojaAplikacja.dll"]
