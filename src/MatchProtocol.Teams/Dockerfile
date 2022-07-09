FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MatchProtocol.Teams/MatchProtocol.Teams.csproj", "MatchProtocol.Teams/"]
RUN dotnet restore "MatchProtocol.Teams/MatchProtocol.Teams.csproj"
COPY . .
WORKDIR "/src/MatchProtocol.Teams"
RUN dotnet build "MatchProtocol.Teams.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MatchProtocol.Teams.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MatchProtocol.Teams.dll"]
