FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "RealEstateApp.csproj"
RUN dotnet build "RealEstateApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RealEstateApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealEstateApp.dll"]
