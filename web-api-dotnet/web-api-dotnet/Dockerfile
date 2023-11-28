FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["web-api-dotnet.csproj", "."]
RUN dotnet restore

COPY . .
WORKDIR /src
RUN dotnet publish "web-api-dotnet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "web-api-dotnet.dll"]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS migrations
WORKDIR /app

COPY ["web-api-dotnet.csproj", "."]
RUN dotnet restore

COPY . .

ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 7.0.0

WORKDIR /app

RUN dotnet ef database update  --context ApplicationDbContext --assembly web-api-dotnet.dll
