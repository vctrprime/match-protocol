FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MatchProtocol.GameSettings/MatchProtocol.GameSettings.csproj", "MatchProtocol.GameSettings/"]
RUN dotnet restore "MatchProtocol.GameSettings/MatchProtocol.GameSettings.csproj"
COPY . .
WORKDIR "/src/MatchProtocol.GameSettings"
RUN dotnet build "MatchProtocol.GameSettings.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MatchProtocol.GameSettings.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MatchProtocol.GameSettings.dll"]
