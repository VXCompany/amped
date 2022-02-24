#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Amped.API/Amped.API.csproj", "Amped.API/"]
COPY ["Amped.Core/Amped.Core.csproj", "Amped.Core/"]
COPY ["Amped.Queries/Amped.Queries.csproj", "Amped.Queries/"]
RUN dotnet restore "Amped.API/Amped.API.csproj"
COPY . .
WORKDIR "/src/Amped.API"
RUN dotnet build "Amped.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amped.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amped.API.dll"]