#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Amped.Profile.API/Amped.Profile.API.csproj", "Amped.Profile.API/"]
RUN dotnet restore "Amped.Profile.API/Amped.Profile.API.csproj"
COPY . .
WORKDIR "/src/Amped.Profile.API"
RUN dotnet build "Amped.Profile.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amped.Profile.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amped.Profile.API.dll"]
