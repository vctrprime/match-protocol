FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MatchProtocol.Games/MatchProtocol.Games.csproj", "MatchProtocol.Games/"]
RUN dotnet restore "MatchProtocol.Games/MatchProtocol.Games.csproj"
COPY . .
WORKDIR "/src/MatchProtocol.Games"
RUN dotnet build "MatchProtocol.Games.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MatchProtocol.Games.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MatchProtocol.Games.dll"]
