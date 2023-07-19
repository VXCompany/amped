FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["amped-bookmark/amped-bookmark.csproj", "amped-bookmark/"]
RUN dotnet restore "amped-bookmark/amped-bookmark.csproj"
COPY . .
WORKDIR "/src/amped-bookmark"
RUN dotnet build "amped-bookmark.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "amped-bookmark.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "amped-bookmark.dll"]
