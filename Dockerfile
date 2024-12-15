# Używamy obrazu .NET SDK do budowania aplikacji
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiowanie plików projektu i przywrócenie zależności
COPY . ./ 
RUN dotnet restore Cykl_zycia_i_narzedzia_devops.sln

# Kopiowanie kodu źródłowego i budowanie aplikacji
COPY . ./ 
RUN dotnet publish /src/Cykl_zycia_i_narzedzia_devops.sln -c Release -o /app

# Używamy lżejszego obrazu do uruchamiania aplikacji
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MojaAplikacja.dll"]