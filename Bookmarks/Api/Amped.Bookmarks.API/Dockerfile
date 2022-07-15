#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Amped.Bookmarks.API/Amped.Bookmarks.API.csproj", "Amped.Bookmarks.API/"]
COPY ["Amped.Bookmarks.Core/Amped.Bookmarks.Core.csproj", "Amped.Bookmarks.Core/"]
COPY ["Amped.Bookmarks.Queries/Amped.Bookmarks.Queries.csproj", "Amped.Bookmarks.Queries/"]
RUN dotnet restore "Amped.Bookmarks.API/Amped.Bookmarks.API.csproj"
COPY . .
WORKDIR "/src/Amped.Bookmarks.API"
RUN dotnet build "Amped.Bookmarks.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amped.Bookmarks.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amped.Bookmarks.API.dll"]
